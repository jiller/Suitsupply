using System.Threading.Tasks;

namespace Suitsupply.Tailoring.Core
{
    /// <summary>
    /// Provide functions to send notifications to customers
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Notify customer about changes in the alteration
        /// </summary>
        /// <param name="customerId">Customer to notify</param>
        /// <param name="alterationId">Changed Alteration</param>
        /// <returns></returns>
        Task NotifyCustomerAsync(int customerId, int alterationId);
    }
}