using System.Linq;
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
    public class GetAlterationListQueryHandler : IQueryHandler<GetAlterationListQuery, Alteration[]>
    {
        private readonly IDbContextFactory<TailoringDbContext> _factory;

        public GetAlterationListQueryHandler(IDbContextFactory<TailoringDbContext> factory)
        {
            _factory = factory;
        }
        
        public async Task<IResult<Alteration[]>> ExecuteAsync(GetAlterationListQuery query)
        {
            using (var db = _factory.Create())
            {
                var alterations = await db.Alterations
                    .OrderByDescending(a => a.Id)
                    .Take(100)
                    .ToArrayAsync();

                return Result.SuccessResult(alterations);
            }
        }
    }
}