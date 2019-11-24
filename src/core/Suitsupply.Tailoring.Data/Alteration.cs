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
        
        public int ShortenSleevesLeft { get; set; }
        
        public int ShortenTrousersLeft { get; set; }
        
        public int ShortenSleevesRight { get; set; }
        
        public int ShortenTrousersRight { get; set; }
        
        public int CustomerId { get; set; }
        
        public AlterationState State { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        public DateTime? PayDate { get; set; }
    }
}