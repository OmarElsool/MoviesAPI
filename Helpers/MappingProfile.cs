using AutoMapper;
using Movies.Dtos;
using Movies.Models;

namespace Movies.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterModel, AppUser>()
                .ForMember(src => src.PasswordHash, opt => opt.Ignore());
            CreateMap<Movie, MoviesDetailsDto>();
            CreateMap<MovieDto, Movie>()
                .ForMember(src => src.Poster, opt => opt.Ignore());
            CreateMap<MovieEditDto, Movie>()
                .ForMember(src => src.Poster, opt => opt.Ignore());
        }
    }
}
