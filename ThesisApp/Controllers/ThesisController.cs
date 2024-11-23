using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ThesisApp.DTO;
using ThesisApp.Interfaces;
using ThesisApp.Models;
using ThesisApp.Repositories;

namespace ThesisApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThesisController : Controller
    {
        private readonly IThesisRepository _thesisRepository;
        private readonly IMapper _mapper;

        public ThesisController(IThesisRepository thesisRepository, IMapper mapper)
        {
            _thesisRepository = thesisRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Thesis>))]
        public IActionResult GetTheses()
        {
            var theses = _mapper.Map<List<ThesisDTO>>(_thesisRepository.GetTheses());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(theses);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Thesis))]
        [ProducesResponseType(400)]
        public IActionResult GetThesis(int id)
        {
            if (!_thesisRepository.ThesisExists(id))
            {
                return NotFound();
            }

            var thesis = _mapper.Map<ThesisDTO>(_thesisRepository.GetThesis(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(thesis);
        }
    }
}
