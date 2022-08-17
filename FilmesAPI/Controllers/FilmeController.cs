using Microsoft.AspNetCore.Mvc;
using FilmesAPI.Models;
using FilmesAPI.Data;
using AutoMapper;
using FilmesAPI.Data.Dtos.Filme;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context; 
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            // O _mapper converte o objeto filmeDto do tipo CreateFilmeDto para o tipo Filme
            Filme filme = _mapper.Map<Filme>(filmeDto);

            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaFilmePorId), new {Id = filme.Id}, filme);
        }

        [HttpGet]
        public IActionResult RecuperaFilmes()
        {
            return Ok(_context.Filmes);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmePorId(int id)
        {

            Filme filme = BuscaFilme(id);

            if (filme is null)
            {
                return NotFound();
            }

            // O _mapper converte o objeto filme do tipo Filme para o tipo ReadFilmeDto
            ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);
            filmeDto.HoraDaConsulta = DateTime.Now;

            return Ok(filmeDto);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto) 
        {
            Filme filme = BuscaFilme(id);

            if (filme is null) 
            {
                return NotFound();
            }

            // Passa os dados de filmeDto para filme
            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            Filme filme = BuscaFilme(id);

            if (filme is null)
            {
                return NotFound();
            }

            _context.Filmes.Remove(filme);
            _context.SaveChanges();

            return NoContent();
        }

        private Filme BuscaFilme(int id)
        {
            return _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        }
    }
}
