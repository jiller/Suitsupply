using System.ComponentModel.DataAnnotations;

namespace Suitsupply.Tailoring.Web.Api.Requests
{
    /// <summary>
    /// Request for alterations
    /// </summary>
    public class AlterationRequest
    {
        /// <summary>
        /// Customer identifier
        /// </summary>
        [Required]
        public int CustomerId { get; set; }
        /// <summary>
        /// Alteration for sleeves
        /// </summary>
        [Required]
        public Shortening ShortenSleeves { get; set; }
        
        /// <summary>
        /// Alteration for trousers
        /// </summary>
        [Required]
        public Shortening ShortenTrousers { get; set; }
    }
}