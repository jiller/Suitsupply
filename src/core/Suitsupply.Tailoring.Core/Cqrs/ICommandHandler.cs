using System.Threading.Tasks;

namespace Suitsupply.Tailoring.Core.Cqrs
{
    public interface ICommandHandler<in TCommand>
        where TCommand: ICommand
    {
        Task<IResult> ExecuteAsync(TCommand command);
    }
}