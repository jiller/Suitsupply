using System.Threading.Tasks;
using JetBrains.Annotations;
using Suitsupply.Tailoring.Core;

namespace Suitsupply.Tailoring.Services
{
    [UsedImplicitly]
    public class NotificationService: INotificationService
    {
        /// <inheritdoc cref="INotificationService"/>
        public Task NotifyCustomerAsync(int customerId, int alterationId)
        {
            return Task.CompletedTask;
        }
    }
}