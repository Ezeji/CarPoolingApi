using System.ComponentModel.DataAnnotations;

namespace CarPoolingCore.Dto
{
    public class CarData
    {
        public string? Name { get; set; }
        public string? Model { get; set; }
        public int NumberOfSeats { get; set; }
        public string? Colour { get; set; }
        public string? PlateNumber { get; set; }
    }

    public class AvailableCarDto
    {
        public string? CarLocation { get; set; }
        public CarData? CarData { get; set; }
    }

    public class AvailableCarRequest
    {
        [Required]
        public int Seats { get; set; }
    }

    public class GroupCarDto : AvailableCarRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
