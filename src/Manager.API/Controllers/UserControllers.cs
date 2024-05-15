using System;
using System.Threading.Tasks;
using Manager.Core.Exceptions;
using Manager.API.ViewModels;
using Manager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Manager.Services.Interfaces;
using AutoMapper;
using Manager.Services.DTO;

namespace Manager.API.Controllers;

[ApiController]

public class UserController : ControllerBase
{
    
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    
    public UserController(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }
    
    [HttpPost]
    [Route("/api/v1/users/create")]

    public async Task<IActionResult> Create([FromBody] CreateUserViewModel userViewModel)
    {
        try
        {
            var userDTO = _mapper.Map<UserDTO>(userViewModel);

            var userCreated = await _userService.Create(userDTO);

            return Ok(new ResultViewModels
            {
                Message = "Usuário criado com sucesso",
                Success = true,
                Data = userCreated
            });

        }
        catch(DomainException ex)
        {
           return BadRequest();
        }
        catch(Exception)
        {
            return StatusCode(500, "Erro");
        }
    }
}
