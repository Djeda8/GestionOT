using AutoMapper;
using DOU.GestionOT.DAL.Entities;

namespace DOU.GestionOT.BL.Dto
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            // Ots
            CreateMap<Ot, OtDto>().ReverseMap();
        }
    }
}
