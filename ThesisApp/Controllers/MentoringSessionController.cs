using Microsoft.AspNetCore.Mvc;
using ThesisApp.Interfaces;
using ThesisApp.Models;
using ThesisApp.Repositories;

namespace ThesisApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentoringSessionController : Controller
    {
        private readonly IMentoringSessionRepository _mentoringSessionRepository;
        public MentoringSessionController(IMentoringSessionRepository mentoringSessionRepository)
        {
            _mentoringSessionRepository = mentoringSessionRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MentoringSession>))]
        public IActionResult GetMentoringSessions()
        {
            var mentoringSessions = _mentoringSessionRepository.GetMentoringSessions();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mentoringSessions);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(MentoringSession))]
        [ProducesResponseType(400)]
        public IActionResult GetMentoringSession(int id)
        {
            if (!_mentoringSessionRepository.MentoringSessionExists(id))
            {
                return NotFound();
            }

            var mentoringSession = _mentoringSessionRepository.GetMentoringSession(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mentoringSession);
        }
    }
}
