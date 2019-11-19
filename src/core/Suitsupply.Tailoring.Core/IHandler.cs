using System.Threading.Tasks;

namespace Suitsupply.Tailoring.Core
{
    public interface IHandler<in TIn, TOut>
    {
        Task<TOut> HandleAsync(TIn input);
    }

    public interface IQueryHandler<in TIn, TOut> : IHandler<TIn, TOut>
        where TIn : IQuery<TOut>
    {
        
    }

    public interface IQuery<out T>
    {
    }

    public interface ICommandHandler<in TIn, TOut> : IHandler<TIn, TOut>
        where TIn: ICommand<TOut>
    {
    }

    public interface ICommand<in TIn> : ICommand
    {
    }

    public interface ICommand
    {
    }
}