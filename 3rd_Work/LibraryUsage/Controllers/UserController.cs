using LibraryUsage.Mapper;
using LibraryUsage.Models;
using LibraryUsage.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryUsage.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userService.GetAll());
        }

        [HttpGet("{userId}")]
        public IActionResult Get(int userId)
        {
            var user = _userService.GetById(userId);

            if (user is null)
            {
                return NotFound("User with such ID was not found");
            }

            return Ok(user);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            var user = await _userService.DeleteById(userId);

            if (user is null)
            {
                return NotFound("User with such ID was not found");
            }

            return Ok(user);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> Update(int userId, CreateUser updateUser)
        {
            var userToUpdate = _userService.GetById(userId);

            if (userToUpdate is null)
            {
                return NotFound("User with such ID was not found");
            }

            var updatedUser = await _userService.Update(userId, updateUser);

            if (updatedUser is null)
            {
                return BadRequest("Invalid user data");
            }

            return Ok(updatedUser);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUser createUser)
        {
            var createdUser = await _userService.Create(createUser);

            if (createdUser is null)
            {
                return BadRequest("Invalid user data");
            }

            return Ok(createdUser);
        }
    }
}
