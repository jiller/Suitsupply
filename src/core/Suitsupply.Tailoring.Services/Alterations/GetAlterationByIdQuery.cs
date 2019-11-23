using Suitsupply.Tailoring.Core.Cqrs;
using Suitsupply.Tailoring.Data;

namespace Suitsupply.Tailoring.Services.Alterations
{
    public class GetAlterationByIdQuery : IQuery<Alteration>
    {
        public int AlterationId { get; }

        public GetAlterationByIdQuery(int alterationId)
        {
            AlterationId = alterationId;
        }
    }
}