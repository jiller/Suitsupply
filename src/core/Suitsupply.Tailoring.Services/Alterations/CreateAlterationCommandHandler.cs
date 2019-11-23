using System.Threading.Tasks;
using Suitsupply.Tailoring.Core;
using Suitsupply.Tailoring.Data;
using Suitsupply.Tailoring.DataAccess;

namespace Suitsupply.Tailoring.Services.Alterations
{
    public class CreateAlterationCommandHandler : ICommandHandler<CreateAlterationCommand>
    {
        private readonly IDbContextFactory<TailoringDbContext> _factory;

        public CreateAlterationCommandHandler(IDbContextFactory<TailoringDbContext> factory)
        {
            _factory = factory;
        }
        
        public async Task<IResult> ExecuteAsync(CreateAlterationCommand command)
        {
            using (var db = _factory.Create())
            {
                var alteration = new Alteration(command.Alteration.CustomerId)
                {
                    ShortenSleeves = command.Alteration.ShortenSleeves,
                    ShortenTrousers = command.Alteration.ShortenTrousers
                };
                db.Alterations.Add(alteration);

                await db.SaveChangesAsync();

                return Result.SuccessResult();
            }
        }
    }
}