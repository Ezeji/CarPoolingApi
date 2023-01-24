using CarPoolingInfrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingCore.Dto
{
    public class CreateJourneyRequest
    {
        [Required]
        public int People { get; set; }

        public string? PickUpLocation { get; set; }

        public string? DestinationLocation { get; set; }
    }

    public class JourneyResponse
    {
        public string? GroupId { get; set; }
        
        public int NumberOfPassengers { get; set; }

        public string? PickUpLocation { get; set; }

        public string? DestinationLocation { get; set; }

        public string? Status { get; set; }
        
        public string? DateCreated { get; set; }

        public string? DateUpdated { get; set; }
    }
}
