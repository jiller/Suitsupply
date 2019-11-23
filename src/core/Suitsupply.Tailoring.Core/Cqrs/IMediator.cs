using System.Threading.Tasks;

namespace Suitsupply.Tailoring.Core.Cqrs
{
    public interface IMediator
    {
        Task<IResult> ExecuteAsync(ICommand command);

        Task<IResult<TResult>> ExecuteAsync<TResult>(IQuery<TResult> query);
    }
}