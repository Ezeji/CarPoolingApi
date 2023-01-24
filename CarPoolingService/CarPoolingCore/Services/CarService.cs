using CarPoolingCore.Common;
using CarPoolingCore.Dto;
using CarPoolingCore.Helpers;
using CarPoolingCore.Mappers;
using CarPoolingCore.Services.Interfaces;
using CarPoolingInfrastructure;
using CarPoolingInfrastructure.Enums;
using CarPoolingInfrastructure.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingCore.Services
{
    public class CarService : ICarService
    {
        private readonly CarPoolingDbContext _carPoolingDbContext;

        public CarService(CarPoolingDbContext carPoolingDbContext)
        {
            _carPoolingDbContext = carPoolingDbContext;
        }

        public async Task<OperationResponse<List<AvailableCarDto>>> UpdateAvailableCarAsync(List<AvailableCarRequest> availableCarRequests)
        {
            //get all available cars
            List<AvailableCar> availableCars = await _carPoolingDbContext.AvailableCars
                                                                         .Find(car => true)
                                                                         .ToListAsync();

            if (availableCars == null)
            {
                return new OperationResponse<List<AvailableCarDto>>()
                                       .SetAsFailureResponse(OperationErrorDictionary.GenericErrorMessage.EntityNotFound());
            }

            foreach (AvailableCar availableCar in availableCars)
            {
                DateTime formattedDate = TextHelpers.FormatDate(availableCar.DateCreated);

                //remove available car record that are not created today
                if (formattedDate.AddDays(1) <= DateTime.UtcNow.Date)
                {
                    await _carPoolingDbContext.AvailableCars
                                              .DeleteOneAsync(car => car.AvailableCarsId == availableCar.AvailableCarsId);
                }
            }

            List<AvailableCarDto> availableCarsResult = new();

            //check for cars with the specified number of seats
            foreach (AvailableCarRequest availableCarRequest in availableCarRequests)
            {
                List<CarDetail> carDetails = await _carPoolingDbContext.CarDetails
                                          .Find(car => car.NumberOfSeats == availableCarRequest.Seats)
                                          .ToListAsync();

                if (carDetails == null)
                {
                    return new OperationResponse<List<AvailableCarDto>>()
                                           .SetAsFailureResponse(OperationErrorDictionary.GenericErrorMessage.EntityNotFound());
                }

                List<AvailableCarDto> freeCars = await GetAvailableCarsAsync(carDetails);
                availableCarsResult.AddRange(freeCars);

                //remove null values as they show that such cars are not available
                availableCarsResult.RemoveAll(car => car == null);
            }

            return new OperationResponse<List<AvailableCarDto>>(availableCarsResult);
        }

        public async Task<OperationResponse<GroupCarDto>> RetrieveGroupCarAsync(string ID)
        {
            //check if journey exists for the specified group
            JourneyDetail journeyDetail = await _carPoolingDbContext.JourneyDetails
                                          .Find(journey => journey.GroupId == ID)
                                          .FirstOrDefaultAsync();

            if (journeyDetail == null)
            {
                return new OperationResponse<GroupCarDto>()
                                       .SetAsFailureResponse(OperationErrorDictionary.CarPoolingErrorMessage.EntityNotFound());
            }

            GroupCarDto groupCarDto = new()
            {
                Id = int.Parse(journeyDetail.GroupId),
                Seats = journeyDetail.NumberOfPassengers
            };

            return new OperationResponse<GroupCarDto>(groupCarDto);
        }

        private async Task<List<AvailableCarDto>> GetAvailableCarsAsync(List<CarDetail> carDetails)
        {
            List<AvailableCarDto> availableCars = new();

            foreach (CarDetail carDetail in carDetails)
            {
                AvailableCar availableCar = await _carPoolingDbContext.AvailableCars
                                          .Find(car => car.CarDetailsId == carDetail.CarDetailsId)
                                          .FirstOrDefaultAsync();

                //map to dto
                CarData carData = CarMapper.MapToDto(carDetail);

                AvailableCarDto availableCarDto;

                if (availableCar == null)
                {
                    availableCarDto = null!;
                }
                else
                {
                    availableCarDto = new()
                    {
                        CarData = carData,
                        CarLocation = availableCar?.CarLocation
                    };
                }

                availableCars.Add(availableCarDto);
            }

            return availableCars;
        }


    }
}
