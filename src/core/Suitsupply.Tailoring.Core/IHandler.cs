using System.Collections.Generic;
using System.Threading.Tasks;

namespace Suitsupply.Tailoring.Core
{
    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<IResult<TResult>> ExecuteAsync(TQuery query);
    }

    public interface IQuery<out TResult>
    {
    }

    public interface ICommandHandler<in TCommand>
        where TCommand: ICommand
    {
        Task<IResult> ExecuteAsync(TCommand command);
    }

    public interface IResult
    {
        bool IsSuccess { get; }
        IEnumerable<string> GetErrors();
    }

    public interface IResult<TResult>
    {
    }

    public interface ICommand
    {
    }

    public class Result : IResult
    {
        private List<string> _errors;

        public Result()
        {
            _errors = new List<string>();
        }
        
        public bool IsSuccess => _errors.Count == 0;

        public IEnumerable<string> GetErrors()
        {
            throw new System.NotImplementedException();
        }

        public static IResult SuccessResult()
        {
            return new Result();
        }
    }
}