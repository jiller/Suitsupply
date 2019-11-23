using System;

namespace Suitsupply.Tailoring.Core
{
    /// <summary>
    /// Provider to retrieve current DateTime
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Gets current DateTime object with time in UTC 
        /// </summary>
        /// <returns></returns>
        DateTime GetUtcNow();
    }
}