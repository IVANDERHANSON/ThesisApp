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
    public class PreThesisController : Controller
    {
        private readonly IPreThesisRepository _preThesisRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public PreThesisController(IPreThesisRepository preThesisRepository, IMapper mapper, IUserRepository userRepository)
        {
            _preThesisRepository = preThesisRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PreThesis>))]
        public IActionResult GetPreTheses()
        {
            var preTheses = _mapper.Map<List<PreThesisDTO>>(_preThesisRepository.GetPreTheses());

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

            var preThesis = _mapper.Map<PreThesisDTO>(_preThesisRepository.GetPreThesis(id));

            return Ok(preThesis);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePreThesis([FromBody] PreThesisCreationDTO preThesisCreationDTO)
        {
            if (preThesisCreationDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.UserExists(preThesisCreationDTO.StudentId))
            {
                return BadRequest(ModelState);
            }

            var user = _userRepository.GetUser(preThesisCreationDTO.StudentId);
            if (user.Role != "Student")
            {
                return BadRequest(ModelState);
            }

            var preThesis = _mapper.Map<PreThesis>(preThesisCreationDTO);
            if (_preThesisRepository.StudentIdExists(preThesis.StudentId) || !_preThesisRepository.CreatePreThesis(preThesis))
            {
                ModelState.AddModelError("", "Something went wrong while creating Pre Thesis.");
                return StatusCode(500, ModelState);
            }

            return Ok(new ResponseDTO
            {
                Message = "Pre Thesis successfully created."
            });
        }
    }
}
