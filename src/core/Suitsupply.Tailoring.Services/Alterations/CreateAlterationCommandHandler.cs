using System.Threading.Tasks;
using Suitsupply.Tailoring.Core;
using Suitsupply.Tailoring.Core.Cqrs;
using Suitsupply.Tailoring.Data;
using Suitsupply.Tailoring.DataAccess;

namespace Suitsupply.Tailoring.Services.Alterations
{
    public class CreateAlterationCommandHandler : ICommandHandler<CreateAlterationCommand>
    {
        private readonly IDbContextFactory<TailoringDbContext> _factory;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateAlterationCommandHandler(IDbContextFactory<TailoringDbContext> factory, IDateTimeProvider dateTimeProvider)
        {
            _factory = factory;
            _dateTimeProvider = dateTimeProvider;
        }
        
        public async Task<IResult> ExecuteAsync(CreateAlterationCommand command)
        {
            using (var db = _factory.Create())
            {
                var alteration = new Alteration(command.Alteration.CustomerId)
                {
                    ShortenSleevesLeft = command.Alteration.ShortenSleevesLeft,
                    ShortenSleevesRight = command.Alteration.ShortenSleevesRight,
                    ShortenTrousersLeft = command.Alteration.ShortenTrousersLeft,
                    ShortenTrousersRight = command.Alteration.ShortenTrousersRight,
                    CreationDate = _dateTimeProvider.GetUtcNow(),
                    State = AlterationState.Created
                };
                db.Alterations.Add(alteration);

                await db.SaveChangesAsync();

                return Result.SuccessResult();
            }
        }
    }
}