using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace TamagotchiApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Farm, FarmDto>();
            CreateMap<Pet, PetDto>();
        }
    }
}
