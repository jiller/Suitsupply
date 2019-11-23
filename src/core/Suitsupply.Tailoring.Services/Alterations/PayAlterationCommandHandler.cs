using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Suitsupply.Tailoring.Core;
using Suitsupply.Tailoring.Data;
using Suitsupply.Tailoring.DataAccess;

namespace Suitsupply.Tailoring.Services.Alterations
{
    public class PayAlterationCommandHandler : ICommandHandler<PayAlterationCommand, Alteration>
    {
        private readonly IDbContextFactory<TailoringDbContext> _factory;
        private readonly IDateTimeProvider _dateTimeProvider;

        public PayAlterationCommandHandler(IDbContextFactory<TailoringDbContext> factory,
            IDateTimeProvider dateTimeProvider)
        {
            _factory = factory;
            _dateTimeProvider = dateTimeProvider;
        }
        
        public async Task<Alteration> HandleAsync(PayAlterationCommand command)
        {
            using (var db = _factory.Create())
            {
                var alteration = await db.Alterations.FirstOrDefaultAsync(a => a.Id == command.AlterationId);
                if (alteration == null)
                {
                    throw new InvalidOperationException($"Alteration with ID='{command.AlterationId}' doesn't exist.");
                }

                // Do nothing, when Alteration already paid
                if (alteration.State == AlterationState.Paid)
                {
                    return alteration;
                }
                
                // Throw exception, when Alteration completed
                if (alteration.State == AlterationState.Done)
                {
                    throw new InvalidOperationException($"Alteration with ID='{command.AlterationId}' already completed");
                }

                alteration.State = AlterationState.Paid;
                alteration.PayDate = _dateTimeProvider.GetUtcNow();

                await db.SaveChangesAsync();
                return alteration;
            }
        }
    }
}