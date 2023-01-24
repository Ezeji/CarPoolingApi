using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingInfrastructure.Models
{
    public class JourneyDetail
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int JourneyDetailsId { get; set; }
        public string? GroupId { get; set; }   //Uniquely generated id for each group on a specific journey
        public int NumberOfPassengers { get; set; }
        public string? PickUpLocation { get; set; }
        public string? DestinationLocation { get; set; }
        public string? DistanceCovered { get; set; }
        public string? Duration { get; set; }
        public string? Status { get; set; }     //1. Created 2. Ongoing 3. Cancelled 4. Completed 
        public string? DateCreated { get; set; }
        public string? DateUpdated { get; set; }
    }
}
