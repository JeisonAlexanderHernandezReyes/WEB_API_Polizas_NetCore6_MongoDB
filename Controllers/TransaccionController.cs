using Amazon.Auth.AccessControlPolicy;
using API_Polizas.Interface;
using API_Polizas.Models;
using API_Polizas.Models.Dto;
using API_Polizas.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Security.Claims;

namespace API_Polizas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionController : ControllerBase
    {
        private readonly ITransaccionService _transaccionService;

        public TransaccionController(ITransaccionService transaccionService)
        {
            _transaccionService = transaccionService;
        }

        /// <summary>
        /// Obtiene las pólizas asociadas a un automóvil según su número de placa.
        /// </summary>
        /// <param name="placa">Número de placa del automóvil.</param>
        /// <returns>Lista de pólizas asociadas al automóvil.</returns>
        /// <response code="200">Se obtuvieron las pólizas exitosamente.</response>
        /// <response code="404">No se encontró el automóvil con la placa especificada.</response>
        /// <exception cref="UnauthorizedAccessException">Se lanza cuando el usuario no está autorizado.</exception>
        [HttpGet("placa/{placa}")]
        public ActionResult<List<Poliza>> GetPolizasByPlaca(string placa)
        {
            UserClient userClient;
            try
            {
                userClient = ValidateAndAuthorize();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    StatusCodeMessage = "No autorizado - No tiene permisos",
                    Result = ""
                });
            }

            try
            {
                var polizas = _transaccionService.GetPolizasByPlaca(placa.ToUpper());

                if (polizas == null)
                {
                    throw new ArgumentException($"No se encontraron pólizas para la placa {placa}");
                }

                return Ok(new
                {
                    StatusCode = StatusCodes.Status200OK,
                    StatusCodeMessage = "OK",
                    Result = polizas
                });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    StatusCodeMessage = "Not Found",
                    Result = ex.Message
                });
            }
        }

        /// <summary>
        /// Obtiene información de una póliza por su identificador.
        /// </summary>
        /// <param name="polizaId">Identificador de la póliza.</param>
        /// <returns>Objeto ActionResult que contiene la información de la póliza si se encuentra, o un mensaje de error si no se encuentra.</returns>
        /// <exception cref="UnauthorizedAccessException">Se lanza cuando el usuario no está autorizado.</exception>
        [HttpGet("poliza/{polizaId}")]
        public ActionResult<PolizaInfoDto> GetPolizaById(string polizaId)
        {

            try
            {
                UserClient userClient = ValidateAndAuthorize();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    StatusCodeMessage = "No autorizado - No tiene permisos",
                    Result = ""
                });
            }

            try
            {
                var poliza = _transaccionService.GetPolizaById(polizaId);

                if (poliza == null)
                {
                    return NotFound();
                }

                var polizaDto = new PolizaInfoDto
                {
                    Id = poliza.Id.ToString(),
                    Nombre = poliza.Nombre,
                    FechaInicio = poliza.FechaInicio.ToString("dd-MM-yyyy"),
                    FechaFinal = poliza.FechaFinal.ToString("dd-MM-yyyy"),
                    Cobertura = poliza.Cobertura,
                    ValorMaxCobertura = poliza.ValorMaxCobertura
                };

                return Ok(new
                {
                    StatusCode = StatusCodes.Status200OK,
                    StatusCodeMessage = "OK",
                    Result = polizaDto
                });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    StatusCodeMessage = "Not Found",
                    Result = ex.Message
                });
            }
        }

        /// <summary>
        /// Obtiene los datos de un automóvil en formato DTO a partir de su identificador.
        /// </summary>
        /// <param name="id">El identificador del automóvil.</param>
        /// <returns>Los datos del automóvil en formato DTO.</returns>
        /// <response code="200">Se obtuvieron los datos del automóvil exitosamente.</response>
        /// <response code="404">No se encontró el automóvil con el identificador especificado.</response>
        /// <exception cref="UnauthorizedAccessException">Se lanza cuando el usuario no está autorizado.</exception>
        [HttpGet(Name = "GetAllAutomotores")]
        public ActionResult<List<AutomotorDto>> GetAllAutomotores()
        {
            try
            {
                UserClient userClient = ValidateAndAuthorize();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    StatusCodeMessage = "No autorizado - No tiene permisos",
                    Result = ""
                });
            }

            try
            {
                var automotores = _transaccionService.GetAllAutomotores();
                if (automotores == null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    StatusCode = StatusCodes.Status200OK,
                    StatusCodeMessage = "OK",
                    Result = automotores
                });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    StatusCodeMessage = "Not Found",
                    Result = ex.Message
                });
            }
        }

        /// <summary>
        /// Crea un nuevo automóvil con las pólizas especificadas.
        /// </summary>
        /// <param name="automotor">El objeto Automotor que contiene los datos del automóvil a crear.</param>
        /// <returns>El objeto Automotor creado.</returns>
        /// <response code="200">Se ha creado el automóvil exitosamente.</response>
        /// <response code="400">No se pudo crear el automóvil debido a un error en los datos proporcionados.</response>
        /// <exception cref="UnauthorizedAccessException">Se lanza cuando el usuario no está autorizado.</exception>
        [HttpPost]
        public ActionResult<AutomotorDto> Create(Automotor automotor)
        {
            try
            {
                UserClient userClient = ValidateAndAuthorize();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    StatusCodeMessage = "No autorizado - No tiene permisos",
                    Result = ""
                });
            }

            try
            {
                var createdAutomotor = _transaccionService.Create(automotor);

                if (createdAutomotor == null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    StatusCode = StatusCodes.Status200OK,
                    StatusCodeMessage = "OK",
                    Result = createdAutomotor
                });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    StatusCodeMessage = "Not Found",
                    Result = ex.Message
                });
            }
        }


        /// <summary>
        /// Agrega pólizas a un automóvil existente.
        /// </summary>
        /// <param name="automotorId">El ID del automóvil.</param>
        /// <param name="newPolicies">La lista de nuevas pólizas a agregar.</param>
        /// <returns>El automóvil actualizado con las pólizas agregadas.</returns>
        /// <response code="200">Se agregaron las pólizas correctamente.</response>
        /// <response code="400">La solicitud es incorrecta debido a un error en los parámetros.</response>
        /// <exception cref="UnauthorizedAccessException">Se lanza cuando el usuario no está autorizado.</exception>
        [HttpPost("{automotorId}/polizas")]
        public ActionResult<AutomotorDto> AddPolizas(string automotorId, [FromBody]  List<Poliza> newPolizas)
        {
            try
            {
                UserClient userClient = ValidateAndAuthorize();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    StatusCodeMessage = "No autorizado - No tiene permisos",
                    Result = ""
                });
            }

            try
            {

                var updatedAutomotor = _transaccionService.AddPolizas(automotorId, newPolizas);

                if (updatedAutomotor == null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    StatusCode = StatusCodes.Status200OK,
                    StatusCodeMessage = "OK",
                    Result = updatedAutomotor
                });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    StatusCodeMessage = "Not Found",
                    Result = ex.Message
                });
            }
        }

        /// <summary>
        /// Valida y autoriza al usuario actual.
        /// </summary>
        /// <returns>Objeto UserClient que representa al usuario autorizado.</returns>
        /// <exception cref="UnauthorizedAccessException">Se lanza cuando el usuario no está autorizado.</exception>
        private UserClient ValidateAndAuthorize()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = Jwt.ValidarToken(identity);

            if (!(rToken.StatusCode == StatusCodes.Status200OK))
            {
                throw new UnauthorizedAccessException();
            }

            UserClient userClient = rToken.Result;

            if (userClient.rol != "ADM")
            {
                throw new UnauthorizedAccessException();
            }

            return userClient;
        }
    }
}
