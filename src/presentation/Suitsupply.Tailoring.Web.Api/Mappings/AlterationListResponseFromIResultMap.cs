using AutoMapper;
using JetBrains.Annotations;
using Suitsupply.Tailoring.Core.Cqrs;
using Suitsupply.Tailoring.Data;
using Suitsupply.Tailoring.Web.Api.Controllers.Requests;
using Suitsupply.Tailoring.Web.Api.Controllers.Responses;

namespace Suitsupply.Tailoring.Web.Api.Mappings
{
    [UsedImplicitly]
    public class AlterationListResponseFromIResultMap : Profile
    {
        public AlterationListResponseFromIResultMap()
        {
            CreateMap<IResult<Alteration[]>, AlterationListResponse>()
                .ForMember(r => r.Alterations, opt => opt.MapFrom(r => r.Data));

            CreateMap<Alteration, AlterationDto>()
                .ForMember(a => a.ShortenSleeves, opt => opt.MapFrom(a => new Shortening(a.ShortenSleevesLeft, a.ShortenSleevesRight)))
                .ForMember(a => a.ShortenTrousers, opt => opt.MapFrom(a => new Shortening(a.ShortenTrousersLeft, a.ShortenTrousersRight)))
                .ReverseMap();
            
            CreateMap<Alteration, Shortening>(MemberList.None)
                .ReverseMap();
        }
    }
}