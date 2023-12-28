using AutoMapper;
using GameReviewApp.Dto;
using GameReviewApp.Models;

namespace GameReviewApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Game, GameDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Country, CountryDto>();
            CreateMap<Producer, ProducerDto>();
            CreateMap<Review, ReviewDto>();
        }
    }
}
