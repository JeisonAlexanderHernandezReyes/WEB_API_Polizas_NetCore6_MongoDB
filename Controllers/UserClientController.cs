using API_Polizas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Polizas.Controllers
{
    [ApiController]
    public class UserClientController : ControllerBase
    {

        public IConfiguration _configuration;

        public UserClientController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("api/IniciarSesion")]
        public dynamic IniciarSesion([FromBody] object optClient)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(optClient.ToString());

            string user = data.usuario.ToString();
            string password = data.password.ToString();

            UserClient clienteUsuario = UserClient.dbUsersJwt().Where(x => x.usuario == user && x.password == password).FirstOrDefault();

            if (clienteUsuario == null)
            {
                return new
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    StatusCodeMessage = "Credenciales incorrectas",
                    Result = ""
                };
            }

            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, clienteUsuario.usuario),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", clienteUsuario.idUsuario),
                new Claim("usuario", clienteUsuario.usuario)
            };  

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));

            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: singIn
                );

            return new
            {
                StatusCode = StatusCodes.Status200OK,
                StatusCodeMessage = "OK",
                Result = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}