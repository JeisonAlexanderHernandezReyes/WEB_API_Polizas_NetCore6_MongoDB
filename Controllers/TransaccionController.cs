using API_Polizas.Models;
using API_Polizas.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API_Polizas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionController : ControllerBase
    {
        private readonly TransaccionService _transaccionService;

        public TransaccionController(TransaccionService transaccionService)
        {
            _transaccionService = transaccionService;
        }

        [HttpGet]
        public ActionResult<List<Automotor>> Get()
        {
            return Ok(_transaccionService.GetAll());
        }

        [HttpGet("{id:length(24)}", Name = "GetAutomotor")]
        public ActionResult<AutomotorDto> GetAutomotorDto(string id)
        {
            var automotor = _transaccionService.GetAutomotorDto(id);

            if (automotor == null)
            {
                return NotFound();
            }

            return automotor;
        }

        [HttpPost]
        public ActionResult<Automotor> Create(Automotor automotor)
        {
            _transaccionService.Create(automotor);

            return CreatedAtRoute("GetAutomotor", new { id = automotor.Id.ToString() }, automotor);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Automotor automotorIn)
        {
            var automotor = _transaccionService.Get(id);

            if (automotor == null)
            {
                return NotFound();
            }

            _transaccionService.Update(id, automotorIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var automotor = _transaccionService.Get(id);

            if (automotor == null)
            {
                return NotFound();
            }

            _transaccionService.Remove(automotor.Id);

            return NoContent();
        }
    }
}
