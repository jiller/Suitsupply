using Suitsupply.Tailoring.Core;
using Suitsupply.Tailoring.Core.Cqrs;

namespace Suitsupply.Tailoring.Services.Alterations
{
    public class CreateAlterationCommand : ICommand
    {
        public NewAlteration Alteration { get; set; }
    }

    public class NewAlteration
    {
        public int Id { get; set; }
        public int ShortenSleevesLeft { get; set; }
        public int ShortenSleevesRight { get; set; }
        public int ShortenTrousersLeft { get; set; }
        public int ShortenTrousersRight { get; set; }
        public int CustomerId { get; set; }
    }
}