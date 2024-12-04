﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ThesisApp.DTO;
using ThesisApp.Interfaces;
using ThesisApp.Models;

namespace ThesisApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDTO>>(_userRepository.GetUsers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int id)
        {
            if (!_userRepository.UserExists(id))
            {
                return NotFound();
            }

            var user = _mapper.Map<UserDTO>(_userRepository.GetUser(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

        [HttpGet("/api/Students")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetStudents()
        {
            var students = _mapper.Map<List<UserDTO>>(_userRepository.GetStudents());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(students);
        }

        [HttpGet("/api/Lecturers")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetLecturers()
        {
            var lecturers = _mapper.Map<List<UserDTO>>(_userRepository.GetLecturers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(lecturers);
        }

        [HttpGet("/api/student-dashboard/{studentId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUserForStudentDashboard(int studentId)
        {
            if (!_userRepository.UserExists(studentId))
            {
                return NotFound();
            }

            if (_userRepository.GetUser(studentId).Role != "Student")
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<UserDTO>(_userRepository.GetUserForStudentDashboard(studentId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

        [HttpGet("/api/lecturer-dashboard/{lecturerId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(400)]
        public IActionResult GetStudentsForLecturerDashboard(int lecturerId)
        {
            if (!_userRepository.UserExists(lecturerId))
            {
                return NotFound();
            }

            if (_userRepository.GetUser(lecturerId).Role != "Lecturer")
            {
                return BadRequest(ModelState);
            }

            var students = _mapper.Map<List<UserDTO>>(_userRepository.GetStudentsForLecturerDashboard(lecturerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(students);
        }
    }
}
