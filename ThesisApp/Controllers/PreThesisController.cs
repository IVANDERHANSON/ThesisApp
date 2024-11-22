using Microsoft.AspNetCore.Mvc;
using ThesisApp.Interfaces;
using ThesisApp.Models;
using ThesisApp.Repositories;

namespace ThesisApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreThesisController : Controller
    {
        private readonly IPreThesisRepository _preThesisRepository;
        public PreThesisController(IPreThesisRepository preThesisRepository)
        {
            _preThesisRepository = preThesisRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PreThesis>))]
        public IActionResult GetPreTheses()
        {
            var preTheses = _preThesisRepository.GetPreTheses();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(preTheses);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(PreThesis))]
        [ProducesResponseType(400)]
        public IActionResult GetPreThesis(int id)
        {
            if (!_preThesisRepository.PreThesisExists(id))
            {
                return NotFound();
            }

            var preThesis = _preThesisRepository.GetPreThesis(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(preThesis);
        }
    }
}
