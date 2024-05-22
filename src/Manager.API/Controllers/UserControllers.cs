using System;
using System.Threading.Tasks;
using Manager.Core.Exceptions;
using Manager.API.ViewModels;
using Manager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Manager.Services.Interfaces;
using Manager.Core.Exceptions;
using System.Collections.Generic;
using Manager.API.Utilities;
using AutoMapper;

using Manager.Services.DTO;
using Microsoft.AspNetCore.Authorization;

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
    //[Authorize]
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
        catch (DomainException ex)
        {
            return BadRequest(Utilities.Response.DomainErrorMessage(ex.Message));
        }

        catch (Exception)
        {
            return StatusCode(500, Utilities.Response.ApplicationErrorMessage());
        }
    }

    [HttpPut]
    //[Authorize]
    [Route("aí/v1/user/update")]
    public async Task<IActionResult> Update([FromBody] UpdateUserViewModel userViewModel)
    {
        try
        {
            var userDTO = _mapper.Map<UserDTO>(userViewModel);

            var userUpdated = await _userService.Update(userDTO);
            return Ok(new ResultViewModels
            {
                Message = "Usuário atualizado com sucesso",
                Success = true,
                Data = userUpdated
            });
        }
        catch (DomainException ex)
        {
            return BadRequest(Utilities.Response.DomainErrorMessage(ex.Message, ex.Errors));
        }
        catch (Exception)
        {
            return StatusCode(500, Utilities.Response.ApplicationErrorMessage());
        }
    }

    [HttpDelete]
    //Authorize
    [Route("api/v1/user/remove/{id}")]
    public async Task<IActionResult> Remove(long id)
    {
        try
        {
            await _userService.Remove(id);
            return Ok(new ResultViewModels
            {
                Message = "Usuárion removido com scuesso",
                Success = true,
                Data = null
            });
        }
        catch (DomainException ex)
        {
            return BadRequest(Utilities.Response.DomainErrorMessage(ex.Message));
        }
        catch (Exception)
        {
            return StatusCode(500, Utilities.Response.ApplicationErrorMessage());
        }
    }

    [HttpGet]
    //[Authorize]
    [Route("api/v1/user/get/{id}")]

    public async Task<IActionResult> Get(long id)
    {
        try
        {
            var user = await _userService.Get(id);

            if (user == null)
                return Ok(new ResultViewModels
                {
                    Message = "Nenhum usuário foi encontrado com o ID infomardo",
                    Success = true,
                    Data = user
                });
            return Ok(new ResultViewModels
            {
                Message = "Usuário encotrado com sucesso",
                Success = true,
                Data = user
            });
        }
        catch (DomainException ex)
        {
            return BadRequest(Utilities.Response.DomainErrorMessage(ex.Message));
        }
        catch (Exception)
        {
            return StatusCode(500, Utilities.Response.ApplicationErrorMessage());
        }
    }

    [HttpGet]
    //[Authorize]
    [Route("api/v1/user/search-by-name")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            try
            {
                var allUsers = await _userService.SearchByName(name);

                if (allUsers.Count == 0)
                    return Ok(new ResultViewModels
                    {
                        Message = "Nenhum usuário encontrado",
                        Success = true,
                        Data = null
                    });
                return Ok(new ResultViewModels
                {
                    Message = "Usuário encotrado com sucesso",
                    Success = true,
                    Data = null
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Utilities.Response.DomainErrorMessage(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Utilities.Response.ApplicationErrorMessage());
            }
        }

    [HttpGet]
    //[Authorize]
    [Route("api/v1/users/search-by-email")]

    public async Task<IActionResult> SearchByEmail([FromQuery] string email)
    {
        try
        {
            var allusers = await _userService.SearchByEmail(email);

            if (allusers.Count == 0)
                return Ok(new ResultViewModels
                {
                    Message = "Nenhum usuário encontrado com o email informado",
                    Success = true,
                    Data = null
                });
            return Ok(new ResultViewModels
            {
                Message = "Usuário encotrado com sucesso",
                Success = true,
                Data = null
            });
        }
        catch (DomainException ex)
        {
            return BadRequest(Utilities.Response.DomainErrorMessage(ex.Message));
        }
        catch
        {
            return StatusCode(500, Utilities.Response.ApplicationErrorMessage());   
        }
    }
}
