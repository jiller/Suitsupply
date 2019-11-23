using System;
using JetBrains.Annotations;

namespace Suitsupply.Tailoring.Data
{
    public class Alteration
    {
        /// <summary>
        /// Create a new instance of Alteration
        /// </summary>
        /// <remarks>
        /// This empty ctor is needed for ORM to project Alteration object from database
        /// </remarks>
        [UsedImplicitly]
        protected Alteration() {}

        public Alteration(int customerId)
        {
            CustomerId = customerId;
        }
        
        public int Id { get; set; }
        
        public byte ShortenSleeves { get; set; }
        
        public byte ShortenTrousers { get; set; }
        
        public int CustomerId { get; set; }
        
        public AlterationState State { get; set; }
        
        public DateTime? PayDate { get; set; }
    }
}