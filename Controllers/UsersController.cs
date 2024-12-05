using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

    [HttpGet]
    public async Task<IActionResult> GetAllUser() => Ok(await _userService.GetAllUsersAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if(user == null) return NotFound();
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(User user)
    {
        var newUser = await _userService.AddUserAsync(user);
        return CreatedAtAction(nameof(GetUserById), new {id = newUser.Id }, newUser);
    }

    [HttpPut ("{id}")]
    public async Task<IActionResult> UpdateUser(int id, User user)
    {
        var updateUsers = await _userService.UpdateUserAsync(id, user);
        if(updateUsers ==  null) return NotFound();
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var success = await _userService.DeleteUserAsync(id);
        if(!success) return NotFound();
        return NoContent();
    }
    }
}