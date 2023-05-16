using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace API_Polizas.Models
{
    public class Poliza
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo FechaInicio es obligatorio")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "El campo FechaFinal es obligatorio")]
        public DateTime FechaFinal { get; set; }

        [Required(ErrorMessage = "El campo Cobertura es obligatorio")]
        public string Cobertura { get; set; }

        [Required(ErrorMessage = "El campo ValorMaxCobertura es obligatorio")]
        public double ValorMaxCobertura { get; set; }
    }
}