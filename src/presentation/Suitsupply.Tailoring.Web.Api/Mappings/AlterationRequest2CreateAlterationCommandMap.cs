using AutoMapper;
using JetBrains.Annotations;
using Suitsupply.Tailoring.Services.Alterations;
using Suitsupply.Tailoring.Web.Api.Controllers.Requests;

namespace Suitsupply.Tailoring.Web.Api.Mappings
{
    [UsedImplicitly]
    public class AlterationRequest2CreateAlterationCommandMap : Profile
    {
        public AlterationRequest2CreateAlterationCommandMap()
        {
            CreateMap<AlterationRequest, NewAlteration>()
                .ReverseMap();

            CreateMap<Shortening, CreateAlterationCommand>(MemberList.None);
            
            CreateMap<AlterationRequest, CreateAlterationCommand>()
                .IncludeMembers(r => r.ShortenSleeves, r => r.ShortenTrousers)
                .ForMember(c => c.Alteration, opt => opt.MapFrom(r => r));
        }
    }
}