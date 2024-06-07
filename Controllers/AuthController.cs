using Microsoft.AspNetCore.Mvc;
using role_play_proj01.Data;
using role_play_proj01.Dtos.User;
using role_play_proj01.Models;

namespace role_play_proj01.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthRepository _authRepo;

    public AuthController(IAuthRepository authRepository)
    {
        _authRepo = authRepository;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDto request)
    {
        ServiceResponse<int> response =
            await _authRepo.Register(new User {Username = request.Username}, request.Password);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto request)
    {
        ServiceResponse<string> response = await _authRepo.Login(request.username, request.password);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}