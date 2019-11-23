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
        public byte ShortenSleeves { get; set; }
        public byte ShortenTrousers { get; set; }
        public int CustomerId { get; set; }
    }
}