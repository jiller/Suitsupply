using Suitsupply.Tailoring.Core.Cqrs;

namespace Suitsupply.Tailoring.Services.Alterations
{
    public class DoneAlterationCommand : ICommand
    {
        public  int AlterationId { get; set; }
    }
}