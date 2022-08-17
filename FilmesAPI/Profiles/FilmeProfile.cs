using AutoMapper;
using FilmesAPI.Data.Dtos.Filme;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class FilmeProfile : Profile
    {
        public FilmeProfile()
        {
            // Mapeia a converção de CreateFilmeDto para Filme
            CreateMap<CreateFilmeDto, Filme>();
            // Mapeia a converção de Filme para ReadFilmeDto
            CreateMap<Filme, ReadFilmeDto>();
            // Mapeia a converção de UpdateFilmeDto para Filme
            CreateMap<UpdateFilmeDto, Filme>();
        }
    }
}
