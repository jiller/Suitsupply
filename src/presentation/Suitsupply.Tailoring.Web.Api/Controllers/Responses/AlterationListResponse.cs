using System;
using Suitsupply.Tailoring.Data;
using Suitsupply.Tailoring.Web.Api.Controllers.Requests;

namespace Suitsupply.Tailoring.Web.Api.Controllers.Responses
{
    public class AlterationListResponse
    {
        public AlterationListResponse()
        {
            Alterations = new AlterationDto[]{};
        }
        
        public AlterationDto[] Alterations { get; set; }
    }

    public class AlterationDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? PayDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public Shortening ShortenSleeves { get; set; }
        public Shortening ShortenTrousers { get; set; }
        public AlterationState State { get; set; }
    }
}