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
        public byte ShortenSleevesLeft { get; set; }
        public byte ShortenSleevesRight { get; set; }
        public byte ShortenTrousersLeft { get; set; }
        public byte ShortenTrousersRight { get; set; }
        public int CustomerId { get; set; }
    }
}