using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Cinema;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public CinemaController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            // O _mapper converte o objeto filmeDto do tipo CreateCinemaDto para o tipo Cinema
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);

            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaCinemaPorId), new {Id = cinema.Id}, cinema);
        }

        [HttpGet]
        public IActionResult RecuperaCinemas()
        {
            return Ok(_context.Cinemas);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaCinemaPorId(int id)
        {
            Cinema cinema = BuscaCinema(id);

            if (cinema is null)
            {
                return NotFound();
            }

            // O _mapper converte o objeto filme do tipo Cinema para o tipo ReadCinemaDto
            ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
            cinemaDto.HoraDaConsulta = DateTime.Now;

            return Ok(cinemaDto);
        }


        [HttpPut("{id}")]
        public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            Cinema cinema = BuscaCinema(id);

            if (cinema is null)
            {
                return NotFound();
            }

            // Passa os dados de cinemaDto para cinema
            _mapper.Map(cinemaDto, cinema);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaCinema(int id)
        {
            Cinema cinema = BuscaCinema(id);

            if (cinema is null)
            {
                return NotFound();
            }

            _context.Cinemas.Remove(cinema);
            _context.SaveChanges();

            return NoContent();
        }

        private Cinema BuscaCinema(int id)
        {
            return _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        }
    }
}
