using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Extensions;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;

namespace ProEventos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _accountService = accountService;
        }

        [HttpGet("GetUser")]	
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var userName = User.GetUserName();
                var user = await _accountService.GetUserByUserNameAsync(userName);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar usuário. Erro: {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]	
        public async Task<IActionResult> Register(UserDto userDto)
        {
            try
            {
                if (await _accountService.UserExists(userDto.UserName))
                    return BadRequest("Usuário já existe.");

                var user = await _accountService.CreateAccountAsync(userDto);
                
                if (user != null)
                    return Ok(user);

                return BadRequest("Usuário não criado, tente novamento mais tarde!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar registrar usuário. Erro: {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]	
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            try
            {
                var user = await _accountService.GetUserByUserNameAsync(userLogin.Username);

                if (user == null) return Unauthorized("Usuário ou Senha está errado.");

                var result = await _accountService.CheckUserPasswordAsync(user, userLogin.Password);

                if (!result.Succeeded) return Unauthorized("Usuário ou Senha está errado.");

                return Ok(new 
                {
                    userName = user.UserName,
                    primeiroNome = user.PrimeiroNome,
                    token = await _tokenService.CreateToken(user)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar realizar login. Erro: {ex.Message}");
            }
        }

        [HttpPut("UpdateUser")]	
        public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _accountService.GetUserByUserNameAsync(User.GetUserName());

                if (user == null) return Unauthorized("Usuário Inválido.");

                var userReturn = await _accountService.UpdateAccount(userUpdateDto);
                
                if (userReturn != null) return NoContent();

                return Ok(userReturn);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar usuário. Erro: {ex.Message}");
            }
        }
    }
}