using AutoMapper;
using Cards.Core.Models;
using Cards.Data.Entities;

namespace Cards.Core.Helpers
{
    /// <summary>
    /// Defines AutoMapper Profiles
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Card, CardDto>().ReverseMap();
            CreateMap<Card, CreateCardDto>().ReverseMap();
            CreateMap<Card, UpdateCardDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
