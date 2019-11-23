using System;
using JetBrains.Annotations;
using Suitsupply.Tailoring.Core;

namespace Suitsupply.Tailoring.Services
{
    [UsedImplicitly]
    public class DateTimeProvider : IDateTimeProvider
    {
        /// <inheritdoc cref="IDateTimeProvider.GetUtcNow"/>
        public DateTime GetUtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}