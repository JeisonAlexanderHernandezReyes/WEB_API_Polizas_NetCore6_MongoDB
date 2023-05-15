using API_Polizas.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Security.Authentication;
using System.Web;

namespace API_Polizas.Services
{
    public class TransaccionService
    {
        private readonly IMongoCollection<Automotor> _automotor;
        private readonly ClienteService _clienteService;

        public TransaccionService(MongoDbContext context, ClienteService clienteService)
        {
            _automotor = context.Automotores;
            _clienteService = clienteService;
        }

        // Create
        public Automotor Create(Automotor automotor)
        {
            _automotor.InsertOne(automotor);
            return automotor;
        }

        // Read
        public AutomotorDto GetAutomotorDto(string id)
        {
            var automotor = _automotor.Find<Automotor>(automotor => automotor.Id == id).FirstOrDefault();

            // Traer el cliente desde MongoDB
            var cliente = _clienteService.Get(automotor.ClientId);

            var automotorDto = new AutomotorDto
            {
                Placa = automotor.Placa,
                TieneInspeccion = automotor.TieneInspeccion,
                Polizas = automotor.Polizas.Select(p => new PolizaDto
                {
                    Nombre = p.Nombre,
                    Cobertura = p.Cobertura,
                    ValorMaxCobertura = p.ValorMaxCobertura,
                    FechaInicio = p.FechaInicio.ToString("dd/MM/yyyy"),
                    FechaFinal = p.FechaFinal.ToString("dd/MM/yyyy"),
                }).ToList(),
                Cliente = new ClienteDto
                {
                    Nombre = cliente.Nombre,
                    NumeroDocumento = cliente.NumeroDocumento,
                    TipoDocumento = cliente.TipoDocumento,
                    CiudadRecidencia = cliente.CiudadRecidencia,
                    DireccionRecidencia = cliente.DireccionRecidencia,
                    FechaNacimiento = cliente.FechaNacimiento,
                }
            };

            return automotorDto;
        }

        public Automotor Get(string id) => _automotor.Find<Automotor>(automotor => automotor.Id == id).FirstOrDefault();

        // Update
        public void Update(string id, Automotor automotorIn) => _automotor.ReplaceOne(automotor => automotor.Id == id, automotorIn);

        // Delete
        public void Remove(Automotor automotorIn) => _automotor.DeleteOne(automotor => automotor.Id == automotorIn.Id);
        public void Remove(string id) => _automotor.DeleteOne(automotor => automotor.Id == id);

        public List<Automotor> GetAll() => _automotor.Find(automotor => true).ToList();

    }
}