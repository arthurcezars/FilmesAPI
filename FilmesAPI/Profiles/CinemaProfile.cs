using AutoMapper;
using FilmesAPI.Data.Dtos.Cinema;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            // Mapeia a converção de CreateCinemaDto para Cinema
            CreateMap<CreateCinemaDto, Cinema>();
            // Mapeia a converção de Cinema para ReadCinemaDto
            CreateMap<Cinema, ReadCinemaDto>();
            // Mapeia a converção de UpdateCinemaDto para Cinema
            CreateMap<UpdateCinemaDto, Cinema>();
        }
    }
}
