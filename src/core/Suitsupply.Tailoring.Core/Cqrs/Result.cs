using System.Collections.Generic;

namespace Suitsupply.Tailoring.Core.Cqrs
{
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