using AutoMapper;
using ThesisApp.DTO;
using ThesisApp.Models;

namespace ThesisApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<PreThesis, PreThesisDTO>();
            CreateMap<PreThesisDTO, PreThesis>();

            CreateMap<MentorPair, MentorPairDTO>();
            CreateMap<MentorPairDTO, MentorPair>();

            CreateMap<MentorPair, MentorPairCreationDTO>();
            CreateMap<MentorPairCreationDTO, MentorPair>();

            CreateMap<MentoringSession, MentoringSessionDTO>();
            CreateMap<MentoringSessionDTO, MentoringSession>();

            CreateMap<Thesis, ThesisDTO>();
            CreateMap<ThesisDTO, Thesis>();

            CreateMap<ThesisDefence, ThesisDefenceDTO>();
            CreateMap<ThesisDefenceDTO, ThesisDefence>();

            CreateMap<ThesisDefence, ThesisDefenceCreationDTO>();
            CreateMap<ThesisDefenceCreationDTO, ThesisDefence>();
        }
    }
}
