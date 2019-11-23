using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace Suitsupply.Tailoring.Web.Api.Requests
{
    /// <summary>
    /// Description of shortening of sleeves or trousers
    /// </summary>
    [UsedImplicitly]
    public class Shortening
    {
        public Shortening()
        {
        }

        public Shortening(byte left, byte right)
        {
            Left = left;
            Right = right;
        }
        
        [Required, Range(-5, 5)]
        public byte Left { get; set; }
        
        [Required, Range(-5, 5)]
        public byte Right { get; set; }
    }
}