using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingInfrastructure.Models
{
    public class CarDetail
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int CarDetailsId { get; set; }
        public string? Name { get; set; }
        public string? Model { get; set; }
        public int NumberOfSeats { get; set; }
        public string? Colour { get; set; }
        public string? PlateNumber { get; set; }
        public string? DateCreated { get; set; }
        public string? DateUpdated { get; set; }
    }
}
