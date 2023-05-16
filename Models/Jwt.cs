using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace API_Polizas.Models
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }

        public static dynamic ValidarToken(ClaimsIdentity claimsIdentity)
        {
            try
            {
                if (claimsIdentity.Claims.Count() == 0)
                {
                    return new
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        StatusCodeMessage = "No autorizado",
                        Result = ""
                    };
                }

                var idClaim = claimsIdentity.FindFirst("id");

                if (idClaim == null)
                {
                    return new
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        StatusCodeMessage = "No autorizado",
                        Result = ""
                    };
                }

                var id = idClaim.Value;

                UserClient userClient = UserClient.dbUsersJwt().FirstOrDefault(x => x.idUsuario == id);

                if (userClient == null)
                {
                    return new
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        StatusCodeMessage = "No autorizado",
                        Result = ""
                    };
                }

                return new
                {
                    StatusCode = StatusCodes.Status200OK,
                    StatusCodeMessage = "Autorizado",
                    Result = userClient
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    StatusCodeMessage = "Error interno del servidor",
                    Result = ex.Message
                };
            }
        }
    }
}