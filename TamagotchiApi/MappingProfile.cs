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
            CreateMap<FarmForCreationDto, Farm>();
            CreateMap<PetForCreationDto, Pet>();
            CreateMap<PetForUpdateDto, Pet>().ReverseMap();
            CreateMap<FarmForUpdateDto, Farm>();
            CreateMap<UserForRegistrationDto, User>()
                .ForMember(u => u.UserName, o => o.MapFrom(x => x.Email));
            CreateMap<User, UserForUpdateDto>();
        }
    }
}
