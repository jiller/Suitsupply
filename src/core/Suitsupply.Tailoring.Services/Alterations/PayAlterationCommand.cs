using Suitsupply.Tailoring.Core;

namespace Suitsupply.Tailoring.Services.Alterations
{
    public class PayAlterationCommand : ICommand
    {
        public int AlterationId { get; set; }
    }
}