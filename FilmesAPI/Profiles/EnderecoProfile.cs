using AutoMapper;
using FilmesAPI.Data.Dtos.Endereco;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class EnderecoProfile : Profile
    {
        public EnderecoProfile()
        {
            // Mapeia a converção de CreateEnderecoDto para Endereco
            CreateMap<CreateEnderecoDto, Endereco>();
            // Mapeia a converção de Endereco para ReadEnderecoDto
            CreateMap<Endereco, ReadEnderecoDto>();
            // Mapeia a converção de UpdateEnderecoDto para Endereco
            CreateMap<UpdateEnderecoDto, Endereco>();
        }
    }
}
