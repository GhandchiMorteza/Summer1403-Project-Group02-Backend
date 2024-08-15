using Microsoft.AspNetCore.Mvc;
using mohaymen_codestar_Team02.Dto.Role;
using mohaymen_codestar_Team02.Dto.User;
using mohaymen_codestar_Team02.Dto.UserDtos;
using mohaymen_codestar_Team02.Dto.UserRole;
using mohaymen_codestar_Team02.Models;
using mohaymen_codestar_Team02.Services.Administration;

namespace mohaymen_codestar_Team02.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        ServiceResponse<List<GetUserDto>> response =
            await _adminService.GetAllUsers();
        return StatusCode((int)response.Type, response.Message);
    }
    
    [HttpGet("GetUserByUsername{username}")]
    public async Task<IActionResult> GetSingle(string username)
    {
        ServiceResponse<GetUserDto> response =
            await _adminService.GetUserByUsername(username);
        return StatusCode((int)response.Type, response.Message);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterUserDto request)
    {
        var user = new User
        {
            Username = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
        };

        ServiceResponse<User> response =
            await _adminService.Register(user, request.Password);

        return StatusCode((int)response.Type, response.Message);
    }
    
    [HttpGet("GetAllRoles")]
    public async Task<IActionResult> GetAllRoles()
    {
        ServiceResponse<List<GetRoleDto>> response =
            await _adminService.GetAllRoles();
        return StatusCode((int)response.Type, response.Message);
    }

    [HttpPut("AddRole")]
    public async Task<IActionResult> AddRole(AddUserRoleDto request)
    {
        ServiceResponse<User> response =
            await _adminService.AddRole(
                new User { Username = request.Username },
                new Role() { RoleType = request.RoleType }
            );

        return StatusCode((int)response.Type, response.Message);
    }

    [HttpPut("DeleteRole")]
    public async Task<IActionResult> DeleteRole(DeleteUserRoleDto request)
    {
        ServiceResponse<User> response =
            await _adminService.DeleteRole(
                new User { Username = request.Username },
                new Role() { RoleType = request.RoleType }
            );

        return StatusCode((int)response.Type, response.Message);
    }
    
    
    

    // test
    [HttpPost("register2Test")]
    public async Task<IActionResult> Register2(RegisterUserDto request)
    {
        ServiceResponse<string> response =
            await _adminService.RegisterRoleTest(
                new User { Username = request.Username },
                request.Password
            );

        return StatusCode((int)response.Type, response.Message);
    }

    [HttpPost("register1Test")]
    public async Task<IActionResult> Register1(RegisterUserDto request)
    {
        ServiceResponse<int> response =
            await _adminService.RegisterUser(new User { Username = request.Username }, request.Password);
        return StatusCode((int)response.Type, response.Message);
    }

    [HttpGet("addRole1Test")]
    public async Task<IActionResult> AddRole1()
    {
        ServiceResponse<string> response =
            await _adminService.AddRoleTest();
        return StatusCode((int)response.Type, response.Message);
    }
}