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
    public class MentorPairController : Controller
    {
        private readonly IMentorPairRepository _mentorPairRepository;
        private readonly IMapper _mapper;

        public MentorPairController(IMentorPairRepository mentorPairRepository, IMapper mapper)
        {
            _mentorPairRepository = mentorPairRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MentorPair>))]
        public IActionResult GetMentorPairs()
        {
            var mentorPairs = _mapper.Map<List<MentorPairDTO>>(_mentorPairRepository.GetMentorPairs());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mentorPairs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(MentorPair))]
        [ProducesResponseType(400)]
        public IActionResult GetMentorPair(int id)
        {
            if (!_mentorPairRepository.MentorPairExists(id))
            {
                return NotFound();
            }

            var mentorPair = _mapper.Map<MentorPairDTO>(_mentorPairRepository.GetMentorPair(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(mentorPair);
        }

        [HttpGet("/api/MentorPair/get-student/{preThesisId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetStudent(int preThesisId)
        {
            var user = _mapper.Map<UserDTO>(_mentorPairRepository.GetStudent(preThesisId));

            if (user == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

        [HttpGet("/api/MentorPair/get-mentor-lecturers")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(400)]
        public IActionResult GetMentorLecturers()
        {
            var users = _mapper.Map<List<UserDTO>>(_mentorPairRepository.GetMentorLecturers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(users);
        }
    }
}
