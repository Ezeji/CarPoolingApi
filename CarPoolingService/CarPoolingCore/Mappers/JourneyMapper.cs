using CarPoolingCore.Dto;
using CarPoolingInfrastructure.Enums;
using CarPoolingInfrastructure.Models;

namespace CarPoolingCore.Mappers
{
    public static class JourneyMapper
    {
        public static JourneyResponse MapToDto(JourneyDetail journeyDetail)
        {
            return new JourneyResponse
            {
                DateCreated = journeyDetail.DateCreated,
                DateUpdated = journeyDetail.DateUpdated,
                DestinationLocation = journeyDetail.DestinationLocation,
                GroupId = journeyDetail.GroupId,
                NumberOfPassengers = journeyDetail.NumberOfPassengers,
                PickUpLocation = journeyDetail.PickUpLocation,
                Status = journeyDetail.Status
            };
        }

        public static JourneyDetail MapFromDto(JourneyResponse journeyResponse)
        {
            return new JourneyDetail
            {
                DateCreated = journeyResponse.DateCreated,
                DateUpdated = journeyResponse.DateUpdated,
                DestinationLocation = journeyResponse.DestinationLocation,
                GroupId = journeyResponse.GroupId,
                NumberOfPassengers = journeyResponse.NumberOfPassengers,
                PickUpLocation = journeyResponse.PickUpLocation,
                Status = journeyResponse.Status
            };
        }
    }
}
