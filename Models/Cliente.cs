using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace API_Polizas.Models
{
    public class Cliente
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [Required(ErrorMessage = "El campo Cobertura es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Documento es obligatorio")]
        public long NumeroDocumento { get; set; }

        [Required(ErrorMessage = "El campo Cobertura es obligatorio")]
        public string TipoDocumento { get; set; }

        [Required(ErrorMessage = "El campo Cobertura es obligatorio")]
        public string CiudadRecidencia { get; set; }

        [Required(ErrorMessage = "El campo Cobertura es obligatorio")]
        public string DireccionRecidencia { get; set; }

        [Required(ErrorMessage = "El campo Cobertura es obligatorio")]
        public string FechaNacimiento { get; set; }
    }
}