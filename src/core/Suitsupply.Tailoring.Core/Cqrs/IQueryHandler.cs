using System.Threading.Tasks;

namespace Suitsupply.Tailoring.Core.Cqrs
{
    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<IResult<TResult>> ExecuteAsync(TQuery query);
    }
}