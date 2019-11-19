using System.Threading.Tasks;
using Suitsupply.Tailoring.Core;
using Suitsupply.Tailoring.Data;

namespace Suitsupply.Tailoring.Services.Alterations
{
    public class PayAlterationCommandHandler : ICommandHandler<PayAlterationCommand, Alteration>
    {
        public async Task<Alteration> HandleAsync(PayAlterationCommand input)
        {
            throw new System.NotImplementedException();
        }
    }
}