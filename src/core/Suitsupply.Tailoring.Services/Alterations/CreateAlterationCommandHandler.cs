using System.Threading.Tasks;
using Suitsupply.Tailoring.Core;
using Suitsupply.Tailoring.Data;
using Suitsupply.Tailoring.DataAccess;

namespace Suitsupply.Tailoring.Services.Alterations
{
    public class CreateAlterationCommandHandler : 
        ICommandHandler<CreateAlterationCommand, NewAlteration>
    {
        private readonly IDbContextFactory<TailoringDbContext> _factory;

        public CreateAlterationCommandHandler(IDbContextFactory<TailoringDbContext> factory)
        {
            _factory = factory;
        }
        
        public async Task<NewAlteration> HandleAsync(CreateAlterationCommand input)
        {
            using (var db = _factory.Create())
            {
                var alteration = new Alteration(input.Alteration.CustomerId)
                {
                    ShortenSleeves = input.Alteration.ShortenSleeves,
                    ShortenTrousers = input.Alteration.ShortenTrousers
                };
                db.Alterations.Add(alteration);

                await db.SaveChangesAsync();

                return new NewAlteration
                {
                    Id = alteration.Id,
                    ShortenSleeves = alteration.ShortenSleeves,
                    ShortenTrousers = alteration.ShortenTrousers,
                    CustomerId = alteration.CustomerId
                };
            }
        }
    }
}