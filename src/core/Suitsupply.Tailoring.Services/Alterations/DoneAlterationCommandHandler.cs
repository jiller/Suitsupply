using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Suitsupply.Tailoring.Core;
using Suitsupply.Tailoring.Core.Cqrs;
using Suitsupply.Tailoring.Data;
using Suitsupply.Tailoring.DataAccess;

namespace Suitsupply.Tailoring.Services.Alterations
{
    [UsedImplicitly]
    public class DoneAlterationCommandHandler : ICommandHandler<DoneAlterationCommand>
    {
        private readonly IDbContextFactory<TailoringDbContext> _factory;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly INotificationService _notificationService;

        public DoneAlterationCommandHandler(
            IDbContextFactory<TailoringDbContext> factory, 
            IDateTimeProvider dateTimeProvider,
            INotificationService notificationService)
        {
            _factory = factory;
            _dateTimeProvider = dateTimeProvider;
            _notificationService = notificationService;
        }
        
        public async Task<IResult> ExecuteAsync(DoneAlterationCommand command)
        {
            using (var db = _factory.Create())
            {
                var alteration = await db.Alterations.FirstOrDefaultAsync(a => a.Id == command.AlterationId);
                if (alteration == null)
                {
                    throw new InvalidOperationException($"Alteration with ID='{command.AlterationId}' doesn't exist.");
                }

                // Do nothing, when Alteration already paid
                if (alteration.State == AlterationState.Done)
                {
                    return Result.SuccessResult();
                }
                
                // Throw exception, when Alteration completed
                if (alteration.State != AlterationState.Paid)
                {
                    throw new InvalidOperationException($"Alteration with ID='{command.AlterationId}' should be paid before done");
                }

                alteration.State = AlterationState.Done;
                alteration.CompleteDate = _dateTimeProvider.GetUtcNow();

                await db.SaveChangesAsync();
                await _notificationService.NotifyCustomerAsync(alteration.CustomerId, alteration.Id);
                return Result.SuccessResult();
            }
        }
    }
}