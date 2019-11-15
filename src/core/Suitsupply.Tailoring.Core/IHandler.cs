namespace Suitsupply.Tailoring.Core
{
    public interface IHandler<in TIn, out TOut>
    {
        TOut Handle(TIn input);
    }

    public interface IQueryHandler<in TIn, out TOut> : IHandler<TIn, TOut>
        where TIn : IQuery<TOut>
    {
        
    }

    public interface IQuery<out T>
    {
    }

    public interface ICommandHandler<in TIn, out TOut> : IHandler<TIn, TOut>
        where TIn: ICommand<TOut>
    {
        
    }

    public interface ICommand<out TOut>
    {
    }
}