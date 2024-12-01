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
    public class ThesisDefenceController : Controller
    {
        private readonly IThesisDefenceRepository _thesisDefenceRepository;
        private readonly IMapper _mapper;
        private readonly IThesisRepository _thesisRepository;
        private readonly IUserRepository _userRepository;

        public ThesisDefenceController(IThesisDefenceRepository thesisDefenceRepository, IMapper mapper, IThesisRepository thesisRepository, IUserRepository userRepository)
        {
            _thesisDefenceRepository = thesisDefenceRepository;
            _mapper = mapper;
            _thesisRepository = thesisRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ThesisDefence>))]
        public IActionResult GetThesisDefences()
        {
            var thesisDefences = _mapper.Map<List<ThesisDefenceDTO>>(_thesisDefenceRepository.GetThesisDefences());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(thesisDefences);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ThesisDefence))]
        [ProducesResponseType(400)]
        public IActionResult GetThesisDefence(int id)
        {
            if (!_thesisDefenceRepository.ThesisDefenceExists(id))
            {
                return NotFound();
            }

            var thesisDefence = _mapper.Map<ThesisDefenceDTO>(_thesisDefenceRepository.GetThesisDefence(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(thesisDefence);
        }

        [HttpGet("/api/ThesisDefence/get-student/{thesisId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetStudent(int thesisId)
        {
            var user = _mapper.Map<UserDTO>(_thesisDefenceRepository.GetStudent(thesisId));

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

        [HttpGet("/api/ThesisDefence/get-examiner-lecturers/{mentorLecturerId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(400)]
        public IActionResult GetExaminerLecturers(int mentorLecturerId)
        {
            if (!_userRepository.UserExists(mentorLecturerId))
            {
                return BadRequest(ModelState);
            }

            if (_userRepository.GetUser(mentorLecturerId).Role != "Lecturer")
            {
                return BadRequest(ModelState);
            }
            
            var examinerLecturers = _mapper.Map<List<UserDTO>>(_thesisDefenceRepository.GetExaminerLecturers(mentorLecturerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(examinerLecturers);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateThesisDefence([FromBody] ThesisDefenceCreationDTO thesisDefenceCreationDTO)
        {
            if (thesisDefenceCreationDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (!_thesisRepository.ThesisExists(thesisDefenceCreationDTO.ThesisId) || !_userRepository.UserExists(thesisDefenceCreationDTO.MentorLecturerId) || !_userRepository.UserExists(thesisDefenceCreationDTO.ExaminerLecturerId))
            {
                return BadRequest(ModelState);
            }

            if (_thesisDefenceRepository.ThesisIdExists(thesisDefenceCreationDTO.ThesisId) || _thesisDefenceRepository.MentorLecturerIdExists(thesisDefenceCreationDTO.MentorLecturerId) || _thesisDefenceRepository.ExaminerLecturerIdExists(thesisDefenceCreationDTO.ExaminerLecturerId))
            {
                return BadRequest(ModelState);
            }

            if (thesisDefenceCreationDTO.MentorLecturerId == thesisDefenceCreationDTO.ExaminerLecturerId)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var thesisDefence = _mapper.Map<ThesisDefence>(thesisDefenceCreationDTO);

            if (!_thesisDefenceRepository.CreateThesisDefence(thesisDefence))
            {
                ModelState.AddModelError("", "Something went wrong while creating Thesis Defence.");
                return StatusCode(500, ModelState);
            }

            return Ok("Thesis Defence successfully created.");
        }
    }
}
