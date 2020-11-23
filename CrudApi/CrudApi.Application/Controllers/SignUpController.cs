using CrudApi.Domain.Models;
using CrudApi.Repository;
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
    public class SignUpController : ControllerBase
    {
        public ClientModel _client;

        [HttpPost]
        public IActionResult SignUp([FromBody]ClientModel client)
        {
            _client = client;
            Context signUp = new Context(_client);
            if(signUp.CheckInDb())
            {
                signUp.InsertClient();
                return Ok(new { message = "Cadastrado!" });
            }
            else
            {
                return BadRequest(new { message = "Usuário já existente" });
            }
        }
    }
}
