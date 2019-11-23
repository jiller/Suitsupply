using Suitsupply.Tailoring.Core;
using Suitsupply.Tailoring.Core.Cqrs;

namespace Suitsupply.Tailoring.Services.Alterations
{
    public class PayAlterationCommand : ICommand
    {
        public int AlterationId { get; set; }
    }
}