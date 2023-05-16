using API_Polizas.Interface;
using API_Polizas.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Security.Authentication;
using System.Web;

namespace API_Polizas.Services
{
    public class TransaccionService : ITransaccionService
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
            if (automotor.Polizas != null)
            {
                foreach (var poliza in automotor.Polizas)
                {
                    if (poliza.FechaFinal < poliza.FechaInicio || poliza.FechaInicio > DateTime.Now || poliza.FechaFinal < DateTime.Now)
                    {
                        throw new ArgumentException("La fecha de inicio de la póliza debe ser anterior a la fecha final y ambas fechas deben estar en el futuro.");
                    }
                }
            }

            _automotor.InsertOne(automotor);
            return automotor;
        }


        // Read
        public AutomotorDto GetAutomotorDto(string id)
        {
            var automotor = _automotor.Find<Automotor>(automotor => automotor.Id == id).FirstOrDefault();

            // Traer el cliente desde MongoDB
            var cliente = _clienteService.Get(ObjectId.Parse(automotor.ClientId));

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

        // Add policies
        public Automotor AddPolizas(string automotorId, List<Poliza> newPolicies)
        {
            var automotor = _automotor.Find<Automotor>(a => a.Id == automotorId).FirstOrDefault();

            if (automotor == null)
            {
                throw new ArgumentException("Automóvil no encontrado.");
            }

            foreach (var poliza in newPolicies)
            {
                if (poliza.FechaFinal < poliza.FechaInicio)
                {
                    throw new ArgumentException("La fecha de inicio de la póliza debe ser anterior a la fecha final y ambas fechas deben estar en el futuro.");
                }
                else if (poliza.FechaInicio < DateTime.Now)
                {
                    throw new ArgumentException("La fecha de la póliza debe ser mayor a la fecha actual: " + TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time")).ToString("dd-MM-yyyy"));
                }

                poliza.Id = ObjectId.GenerateNewId().ToString();
            }

            automotor.Polizas.AddRange(newPolicies);
            _automotor.ReplaceOne(a => a.Id == automotorId, automotor);

            return automotor;
        }

        // Get by plate number
        public List<Poliza> GetPolizasByPlaca(string placa)
        {
            var automotor = _automotor.Find<Automotor>(a => a.Placa == placa).FirstOrDefault();

            if (automotor == null)
            {
                throw new ArgumentException("Automóvil no encontrado.");
            }

            return automotor.Polizas;
        }

        // Get by policy number
        public Poliza GetPolizaById(string polizaId)
        {
            var automotor = _automotor.Find<Automotor>(a => a.Polizas.Any(p => p.Id.ToString() == polizaId)).FirstOrDefault();

            if (automotor == null)
            {
                throw new ArgumentException("Automóvil no encontrado.");
            }

            return automotor.Polizas.FirstOrDefault(p => p.Id.ToString() == polizaId);
        }

        // Get all automotores
        public List<AutomotorDto> GetAllAutomotores()
        {
            var automotores = _automotor.Find(automotor => true).ToList();

            var automotoresDto = automotores.Select(automotor =>
            {
                var cliente = _clienteService.Get(ObjectId.Parse(automotor.ClientId));

                return new AutomotorDto
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
            }).ToList();

            return automotoresDto;
        }
    }
}