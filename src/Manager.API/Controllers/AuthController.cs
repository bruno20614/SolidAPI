using Manager.API.Token;
using Manager.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Manager.API.Utilities;

namespace Manager.API.Controllers;
    [ApiController]


public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ITokenGenerator _tokenGenerator;
    
    public AuthController(IConfiguration configuration, ITokenGenerator tokenGenerator)
    {
        _configuration = configuration;
        _tokenGenerator = tokenGenerator;
    }

    [HttpPost]
    [Route("/api/v1/auth/login")]
    public IActionResult Login([FromBody] LoginViewModel loginViewModel)
    {
        try
        {
            var tokenLogin = _configuration["Jwt:Login"];
            var tokenPassword = _configuration["Jwt:Password"];

            if (loginViewModel.Login == tokenLogin && loginViewModel.Password == tokenPassword)
            {
                return Ok(new ResultViewModels
                {
                    Message ="Usuário autenticado com sucesso",
                    Success = true,
                    Data = new
                    {
                        Token = _tokenGenerator.GenerateToken(),
                        TokenExpires=DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:HoursToExpire"]))
                    }
                });
            }
            else
            {
                return StatusCode(401, Utilities.Response.UnauthorizedErrorMessage());
            }
        }
        catch (Exception)
        {
            return StatusCode(500, Utilities.Response.ApplicationErrorMessage());
        }
    }
}