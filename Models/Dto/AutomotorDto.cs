using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API_Polizas.Models
{
    public class AutomotorDto
    {
        public string Placa { get; set; }
        public bool TieneInspeccion { get; set; }

        public List<PolizaDto> Polizas { get; set; }

        public ClienteDto Cliente { get; set; }
    }

    public class PolizaDto
    {
        public string Nombre { get; set; }
        public string Cobertura { get; set; }
        public double ValorMaxCobertura { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFinal { get; set; }
    }

    public class ClienteDto
    {
        public string Nombre { get; set; }
        public long NumeroDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public string CiudadRecidencia { get; set; }
        public string DireccionRecidencia { get; set; }
        public string FechaNacimiento { get; set; }
    }
}
