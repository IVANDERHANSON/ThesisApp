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
        private readonly IUserRepository _userRepository;
        private readonly IPreThesisRepository _preThesisRepository;
        private readonly IMentorPairRepository _mentorPairRepository;
        private readonly IMentoringSessionRepository _mentoringSessionRepository;

        public ThesisController(IThesisRepository thesisRepository, IMapper mapper, IUserRepository userRepository, IPreThesisRepository preThesisRepository, IMentorPairRepository mentorPairRepository, IMentoringSessionRepository mentoringSessionRepository)
        {
            _thesisRepository = thesisRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _preThesisRepository = preThesisRepository;
            _mentorPairRepository = mentorPairRepository;
            _mentoringSessionRepository = mentoringSessionRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Thesis>))]
        public IActionResult GetTheses()
        {
            var theses = _mapper.Map<List<ThesisDTO>>(_thesisRepository.GetTheses());

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

            return Ok(thesis);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateThesis([FromBody] ThesisCreationDTO thesisCreationDTO)
        {
            if (thesisCreationDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.UserExists(thesisCreationDTO.StudentId))
            {
                return BadRequest(ModelState);
            }

            var user = _userRepository.GetUser(thesisCreationDTO.StudentId);
            if (user.Role != "Student")
            {
                return BadRequest(ModelState);
            }

            if (!_preThesisRepository.StudentIdExists(thesisCreationDTO.StudentId))
            {
                return BadRequest(ModelState);
            }

            var preThesis = _preThesisRepository.GetPreThesisByStudentId(thesisCreationDTO.StudentId);

            if (!_mentorPairRepository.PreThesisIdExists(preThesis.id))
            {
                return BadRequest(ModelState);
            }

            var mentorPair = _mentorPairRepository.GetMentorPairByPreThesisId(preThesis.id);

            if (_mentoringSessionRepository.CountMentoringSessions(mentorPair.id) < 3)
            {
                ModelState.AddModelError("", "You must have at least 3 Mentoring Sessions to create a Thesis.");
                return StatusCode(500, ModelState);
            }

            var thesis = _mapper.Map<Thesis>(thesisCreationDTO);
            if (_thesisRepository.StudentIdExists(thesis.StudentId) || !_thesisRepository.CreateThesis(thesis))
            {
                ModelState.AddModelError("", "Something went wrong while creating Thesis.");
                return StatusCode(500, ModelState);
            }

            return Ok(new ResponseDTO
            {
                Message = "Thesis successfully created."
            });
        }
    }
}
