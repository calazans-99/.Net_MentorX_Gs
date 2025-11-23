
using AutoMapper;

namespace Mentorax.Api.Services.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Mentorax.Api.Models.Mentor, Mentorax.Api.Models.Dto.MentorDto>().ReverseMap();
            CreateMap<Mentorax.Api.Models.Mentorado, Mentorax.Api.Models.Dto.MentoradoDto>().ReverseMap();
            CreateMap<Mentorax.Api.Models.PlanoMentoria, Mentorax.Api.Models.Dto.PlanoMentoriaDto>().ReverseMap();
            CreateMap<Mentorax.Api.Models.Questionario, Mentorax.Api.Models.Dto.QuestionarioDto>().ReverseMap();
            CreateMap<Mentorax.Api.Models.TarefaMentoria, Mentorax.Api.Models.Dto.TarefaMentoriaDto>().ReverseMap();

        }
    }
}
