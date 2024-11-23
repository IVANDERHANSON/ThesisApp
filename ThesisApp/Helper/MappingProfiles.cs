using AutoMapper;
using ThesisApp.DTO;
using ThesisApp.Models;

namespace ThesisApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Thesis, ThesisDTO>();
            CreateMap<ThesisDTO, Thesis>();

            CreateMap<ThesisDefence, ThesisDefenceDTO>();
            CreateMap<ThesisDefenceDTO, ThesisDefence>();

            CreateMap<ThesisDefence, ThesisDefenceCreationDTO>();
            CreateMap<ThesisDefenceCreationDTO, ThesisDefence>();
        }
    }
}
