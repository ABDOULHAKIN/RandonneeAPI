using AutoMapper;
using NewZWalkAPI.Models.Domain;
using NewZWalkAPI.Models.DTO;

namespace NewZWalkAPI.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddRegionRequestDTO, RegionDTO>().ReverseMap();
            CreateMap<UpdateRegionRequestDTO, RegionDTO>().ReverseMap();
        }
    }
}
