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
        private readonly IPreThesisRepository _preThesisRepository;
        private readonly IUserRepository _userRepository;

        public MentorPairController(IMentorPairRepository mentorPairRepository, IMapper mapper, IPreThesisRepository preThesisRepository, IUserRepository userRepository)
        {
            _mentorPairRepository = mentorPairRepository;
            _mapper = mapper;
            _preThesisRepository = preThesisRepository;
            _userRepository = userRepository;
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
            if (_preThesisRepository.PreThesisExists(preThesisId) && !_mentorPairRepository.PreThesisIdExists(preThesisId))
            {
                var user = _mapper.Map<UserDTO>(_mentorPairRepository.GetStudent(preThesisId));

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(user);
            } else {
                return NotFound();
            }
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMentorPair([FromBody] MentorPairCreationDTO mentorPairCreationDTO)
        {
            if (mentorPairCreationDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (!_preThesisRepository.PreThesisExists(mentorPairCreationDTO.PreThesisId) || !_userRepository.UserExists(mentorPairCreationDTO.MentorLecturerId))
            {
                return BadRequest(ModelState);
            }

            var user = _userRepository.GetUser(mentorPairCreationDTO.MentorLecturerId);
            if (user.Role != "Lecturer")
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mentorPair = _mapper.Map<MentorPair>(mentorPairCreationDTO);

            if (_mentorPairRepository.PreThesisIdExists(mentorPair.PreThesisId) || _mentorPairRepository.MentorLecturerIdExists(mentorPair.MentorLecturerId) || !_mentorPairRepository.CreateMentorPair(mentorPair))
            {
                ModelState.AddModelError("", "Something went wrong while creating Mentor Pair.");
                return StatusCode(500, ModelState);
            }

            return Ok(new ResponseDTO
            {
                Message = "Mentor Pair successfully created."
            });
        }

        [HttpGet("/api/MentorPair/edit/get-student/{studentId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetStudentForEditMentorPair(int studentId)
        {
            if (_userRepository.UserExists(studentId))
            {
                if(_userRepository.GetUser(studentId).Role == "Student")
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    var user = _mapper.Map<UserDTO>(_mentorPairRepository.GetStudentForEditMentorPair(studentId));

                    if (user.PreThesis == null || user.PreThesis.MentorPair == null)
                    {
                        return BadRequest(ModelState);
                    }

                    return Ok(user);
                } else
                {
                    return BadRequest(ModelState);
                }
            } else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("/api/MentorPair/edit/{mentorPairId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMentorPair(int mentorPairId, [FromBody] MentorPairCreationDTO updatedMentorPair)
        {
            if (updatedMentorPair == null)
            {
                return BadRequest(ModelState);
            }

            if (!_mentorPairRepository.MentorPairExists(mentorPairId)) {
                return NotFound();
            }

            if (!_preThesisRepository.PreThesisExists(updatedMentorPair.PreThesisId) || !_userRepository.UserExists(updatedMentorPair.MentorLecturerId))
            {
                return BadRequest(ModelState);
            }

            var user = _userRepository.GetUser(updatedMentorPair.MentorLecturerId);
            if (user.Role != "Lecturer")
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mentorPair = _mapper.Map<MentorPair>(updatedMentorPair);
            var oldMentorPair = _mentorPairRepository.GetMentorPair(mentorPairId);
            var validated = false;

            if (mentorPair.PreThesisId != oldMentorPair.PreThesisId)
            {
                validated = false;
            } else if (mentorPair.MentorLecturerId == oldMentorPair.MentorLecturerId)
            {
                validated = _mentorPairRepository.SameMentorLecturerId(oldMentorPair, mentorPair);
            } else if (_mentorPairRepository.MentorLecturerIdExists(mentorPair.MentorLecturerId))
            {
                validated = false;
            } else
            {
                validated = _mentorPairRepository.UpdateMentorPair(oldMentorPair, mentorPair);
            }

            if (validated)
            {
                return Ok(new ResponseDTO
                {
                    Message = "Mentor Pair successfully updated."
                });
            } else
            {
                ModelState.AddModelError("", "Something went wrong while updating Mentor Pair.");
                return StatusCode(500, ModelState);
            }
        }
    }
}
