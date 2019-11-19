namespace Suitsupply.Tailoring.Data
{
    public class Alteration
    {
        protected Alteration() {}

        public Alteration(int customerId)
        {
            CustomerId = customerId;
        }
        
        public int Id { get; set; }
        
        public byte ShortenSleeves { get; set; }
        
        public byte ShortenTrousers { get; set; }
        
        public int CustomerId { get; set; }
    }
}