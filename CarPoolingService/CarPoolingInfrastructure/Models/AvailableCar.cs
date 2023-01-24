using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingInfrastructure.Models
{
    public class AvailableCar
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int AvailableCarsId { get; set; }
        public int CarDetailsId { get; set; }
        public int? JourneyDetailsId { get; set; }
        public string? CarLocation { get; set; }
        public string? DateCreated { get; set; }
        public string? DateUpdated { get; set; }
    }
}
