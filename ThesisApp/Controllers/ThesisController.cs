using Microsoft.AspNetCore.Mvc;
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
        public ThesisController(IThesisRepository thesisRepository)
        {
            _thesisRepository = thesisRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Thesis>))]
        public IActionResult GetTheses()
        {
            var theses = _thesisRepository.GetTheses();

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

            var thesis = _thesisRepository.GetThesis(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(thesis);
        }
    }
}
