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
    }
}
