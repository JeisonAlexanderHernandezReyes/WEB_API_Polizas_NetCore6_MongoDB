﻿using MongoDB.Bson.Serialization.Attributes;
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
        [RegularExpression("^[A-Za-z]{3}[0-9]{3}$", ErrorMessage = "La placa debe tener el formato AAA999")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "El campo TieneInspeccion es obligatorio")]
        public bool TieneInspeccion { get; set; }

        [BsonElement("Poliza")] 
        public List<Poliza> Polizas { get; set; }

        [BsonElement("clientID")]  
        [BsonRepresentation(BsonType.ObjectId)]
        public string ClientId { get; set; }
    }
}
