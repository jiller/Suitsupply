using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;

namespace Suitsupply.Tailoring.Web.Api.HostedServices
{
    public interface IQueueListenerService : IHostedService
    {
        Task ProcessOrderPaidMessageAsync(Message message, CancellationToken cancellationToken);
    }
}