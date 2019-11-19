using System;
using Suitsupply.Tailoring.Core;
using Suitsupply.Tailoring.Data;

namespace Suitsupply.Tailoring.Services.Alterations
{
    public class PayAlterationCommand : ICommand<Alteration>
    {
        public int AlterationId { get; set; }
    }
}