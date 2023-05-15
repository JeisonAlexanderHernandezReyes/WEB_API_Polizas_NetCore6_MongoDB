using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace API_Polizas.Models
{
    public class Automotor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required(ErrorMessage = "El campo Placa es obligatorio")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "El campo TieneInspeccion es obligatorio")]
        public bool TieneInspeccion { get; set; }

        [BsonElement("Poliza")] 
        public List<Poliza> Polizas { get; set; }

        [BsonElement("clientID")]  
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId ClientId { get; set; }
    }
}
