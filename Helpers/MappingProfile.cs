using AutoMapper;
using Movies.Dtos;
using Movies.Models;

namespace Movies.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MoviesDetailsDto>();
            CreateMap<MovieDto, Movie>()
                .ForMember(src => src.Poster, opt => opt.Ignore());
            CreateMap<MovieEditDto, Movie>()
                .ForMember(src => src.Poster, opt => opt.Ignore());
        }
    }
}
