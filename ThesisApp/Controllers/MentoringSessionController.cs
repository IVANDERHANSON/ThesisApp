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
    public class MentoringSessionController : Controller
    {
        private readonly IMentoringSessionRepository _mentoringSessionRepository;
        private readonly IMentorPairRepository _mentorPairRepository;
        private readonly IMapper _mapper;

        public MentoringSessionController(IMentoringSessionRepository mentoringSessionRepository, IMentorPairRepository mentorPairRepository, IMapper mapper)
        {
            _mentoringSessionRepository = mentoringSessionRepository;
            _mentorPairRepository = mentorPairRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MentoringSession>))]
        public IActionResult GetMentoringSessions()
        {
            var mentoringSessions = _mapper.Map<List<MentoringSessionDTO>>(_mentoringSessionRepository.GetMentoringSessions());

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

            var mentoringSession = _mapper.Map<MentoringSessionDTO>(_mentoringSessionRepository.GetMentoringSession(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mentoringSession);
        }

        [HttpGet("/api/MentoringSession/mentor-pair/{mentorPairId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MentoringSession>))]
        [ProducesResponseType(400)]
        public IActionResult GetMentoringSessionsByMentorPairId(int mentorPairId)
        {
            if (!_mentorPairRepository.MentorPairExists(mentorPairId))
            {
                return NotFound();
            }

            var mentoringSessions = _mapper.Map<List<MentoringSessionDTO>>(_mentoringSessionRepository.GetMentoringSessionsByMentorPairId(mentorPairId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mentoringSessions);
        }
    }
}
