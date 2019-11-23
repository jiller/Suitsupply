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
    public class GetAlterationByIdQueryHandler : IQueryHandler<GetAlterationByIdQuery, Alteration>
    {
        private readonly IDbContextFactory<TailoringDbContext> _factory;

        public GetAlterationByIdQueryHandler(IDbContextFactory<TailoringDbContext> _factory)
        {
            this._factory = _factory;
        }
        
        public async Task<IResult<Alteration>> ExecuteAsync(GetAlterationByIdQuery query)
        {
            using (var db = _factory.Create())
            {
                var alteration = await db.Alterations.FirstOrDefaultAsync(a => a.Id == query.AlterationId);
                return Result.SuccessResult(alteration);
            }
        }
    }
}