using CrudApi.Domain.Models;
using CrudApi.Repository;
using CrudApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CrudApi.Application.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly TokenService _tokenService;
        public LoginController(TokenService tokenService)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login([FromBody] ClientModel client)
        {
            var result = _tokenService.GenerateToken(client);
            return Ok(result);     
        }

        [HttpPut]
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
