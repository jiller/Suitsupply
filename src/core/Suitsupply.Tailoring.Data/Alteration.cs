namespace Suitsupply.Tailoring.Data
{
    public class Alteration
    {
        protected Alteration() {}

        public Alteration(byte shortenSleeves, byte shortenTrousers)
        {
            ShortenSleeves = shortenSleeves;
            ShortenTrousers = shortenTrousers;
        }
        
        public byte ShortenSleeves { get; set; }
        
        public byte ShortenTrousers { get; set; }
        public int Id { get; set; }
    }
}