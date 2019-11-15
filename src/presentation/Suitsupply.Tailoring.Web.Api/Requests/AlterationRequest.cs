using System.ComponentModel.DataAnnotations;

namespace Suitsupply.Tailoring.Web.Api.Requests
{
    /// <summary>
    /// Request for alterations
    /// </summary>
    public class AlterationRequest
    {
        /// <summary>
        /// Alteration for sleeves
        /// </summary>
        [Required, Range(-5, 5)]
        public byte ShortenSleeves { get; set; }
        
        /// <summary>
        /// Alteration for trousers
        /// </summary>
        [Required, Range(-5, 5)]
        public byte ShortenTrousers { get; set; }
    }
}