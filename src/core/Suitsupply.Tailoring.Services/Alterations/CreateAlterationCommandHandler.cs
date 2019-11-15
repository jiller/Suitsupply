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
        
        public NewAlteration Handle(CreateAlterationCommand input)
        {
            using (var db = _factory.Create())
            {
                var alteration = new Alteration(input.Alteration.ShortenSleeves, input.Alteration.ShortenTrousers);
                db.Alterations.Add(alteration);

                db.SaveChanges();

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