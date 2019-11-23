using System.Collections.Generic;

namespace Suitsupply.Tailoring.Core.Cqrs
{
    public interface IResult
    {
        bool IsSuccess { get; }
        IEnumerable<string> GetErrors();
    }
    
    public interface IResult<TResult>
    {
        TResult Data { get; }
    }
}