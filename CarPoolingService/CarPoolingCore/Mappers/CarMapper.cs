using CarPoolingCore.Dto;
using CarPoolingInfrastructure.Models;

namespace CarPoolingCore.Mappers
{
    public static class CarMapper
    {
        public static CarData MapToDto(CarDetail carDetail)
        {
            return new CarData
            {
                Colour = carDetail.Colour,
                Model = carDetail.Model,
                Name = carDetail.Name,
                NumberOfSeats = carDetail.NumberOfSeats,
                PlateNumber = carDetail.PlateNumber
            };
        }

        public static CarDetail MapFromDto(CarData carData)
        {
            return new CarDetail
            {
                Colour = carData.Colour,
                Model = carData.Model,
                Name = carData.Name,
                NumberOfSeats = carData.NumberOfSeats,
                PlateNumber = carData.PlateNumber
            };
        }
    }
}
