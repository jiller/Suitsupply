using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace Suitsupply.Tailoring.Web.Api.Messaging
{
    public interface IQueuedMessageHandler
    {
        Task ProcessOrderPaidMessageAsync(Message message, CancellationToken cancellationToken);
        Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg);
    }
}