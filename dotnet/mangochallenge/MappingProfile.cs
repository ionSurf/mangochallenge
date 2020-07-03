using AutoMapper;
using Entities.Models;
using Entities.DataTransferObjects;

namespace MangoChallenge
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Portrait
            CreateMap<Portrait, PortraitDto>();
            CreateMap<PortraitForCreationDto,Portrait>();
            CreateMap<PortraitForUpdateDto, Portrait>();
        }
    }
}