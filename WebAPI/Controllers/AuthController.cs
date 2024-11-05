using ApiContracts.DTOs;
using Microsoft.AspNetCore.Mvc;
using Shared.ApiContracts;
using EntityRepository;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepo _userRepo;

    public AuthController(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginRequest request)
    {
        var user = await _userRepo.GetUserByUsernameAsync(request.UserName);
        if (user == null)
        {
            return Unauthorized("Invalid username or password");
        }

        if (user.Password != request.Password)
        {
            return Unauthorized("Invalid username or password");
        }

        var userDto = new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
        };

        return Ok(userDto);
    }
}