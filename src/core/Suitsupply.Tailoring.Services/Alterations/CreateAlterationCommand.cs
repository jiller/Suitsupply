using Suitsupply.Tailoring.Core;
using Suitsupply.Tailoring.Data;

namespace Suitsupply.Tailoring.Services.Alterations
{
    public class CreateAlterationCommand : ICommand<NewAlteration>
    {
        public NewAlteration Alteration { get; set; }
    }

    public class NewAlteration
    {
        public int Id { get; set; }
        
        public byte ShortenSleeves { get; set; }
        
        public byte ShortenTrousers { get; set; }
    }
}