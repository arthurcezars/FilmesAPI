using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Endereco;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public EnderecoController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }

        [HttpPost]
        public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            // O _mapper converte o objeto filmeDto do tipo CreateEnderecoDto para o tipo Endereco
            Endereco endereco = _mapper.Map<Endereco>(enderecoDto);

            _context.Enderecos.Add(endereco);
            _context.SaveChanges();

            return CreatedAtAction(nameof(RecuperaEnderecoPorId), new {Id = endereco.Id}, endereco);
        }

        [HttpGet]
        public IActionResult RecuperaEnderecos()
        {
            return Ok(_context.Enderecos);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaEnderecoPorId(int id)
        {
            Endereco endereco = BuscaEndereco(id);

            if (endereco is null)
            {
                return NotFound();
            }

            // O _mapper converte o objeto endereco do tipo Endereco para o tipo ReadEnderecoDto
            ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);
            enderecoDto.HoraDaConsulta = DateTime.Now;

            return Ok(enderecoDto);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto)
        {
            Endereco endereco = BuscaEndereco(id);

            if (endereco is null)
            {
                return NotFound();
            }

            // Passa os dados de enderecoDto para endereco
            _mapper.Map(enderecoDto, endereco);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaEndereco(int id)
        {
            Endereco endereco = BuscaEndereco(id);
            
            if (endereco is null)
            {
                return NotFound();
            }

            _context.Enderecos.Remove(endereco);
            _context.SaveChanges();

            return NoContent();
        }

        private Endereco BuscaEndereco(int id)
        {
            return _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
        }
    }
}
