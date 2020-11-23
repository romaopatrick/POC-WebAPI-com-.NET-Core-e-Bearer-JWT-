using CrudApi.Domain.Models;
using CrudApi.Repository;
using CrudApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApi.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly TokenService _tokenService;
        public LoginController(TokenService tokenService)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpGet]
        public IActionResult Login([FromBody] ClientModel client)
        {
            Context login = new Context(client);
            if (login.CheckInDb() == false)
            {
                var result = _tokenService.GenerateToken(client);
                return Ok(result);
            }
            else
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }
        }
        [HttpPut]
        [Route("update")]
        [AllowAnonymous] //Não consegui utilizar o authorize porque nao criei um atributo pra diferenciar o nivel de autorizacao :c
        public IActionResult UpdateClient([FromBody] ClientModel newclient)
        {
            Context update = new Context(User.Identity.Name);
            if(update.Update(newclient))
            {
                return Ok(new { message = "Alterado com sucesso" });
            }
            else
            {
                return BadRequest(new { message = "Não foi possivel alterar" });
            }
        }
        [HttpDelete]
        [Route("delete")]
        [AllowAnonymous]
        public IActionResult DeleteClient()
        {
            Context delete = new Context(User.Identity.Name);
            if(delete.DeleteClient())
            {
                return Ok(new { message = "Deletado com sucesso" });
            }
            else
            {
                return BadRequest(new { message = "Não foi possivel deletar" });
            }
        }
    }
}
