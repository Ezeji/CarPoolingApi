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
    public class JourneyService : IJourneyService
    {
        private readonly CarPoolingDbContext _carPoolingDbContext;

        public JourneyService(CarPoolingDbContext carPoolingDbContext)
        {
            _carPoolingDbContext = carPoolingDbContext;
        }

        public async Task<OperationResponse<JourneyResponse>> CreateJourneyAsync(CreateJourneyRequest journeyRequest)
        {
            JourneyDetail journeyDetail = new()
            {
                DateCreated = DateTime.UtcNow.ToString("dd-MM-yyyy"),
                DateUpdated = DateTime.UtcNow.ToString("dd-MM-yyyy"),
                DestinationLocation = journeyRequest.DestinationLocation,
                GroupId = Guid.NewGuid().ToString(),
                JourneyDetailsId = int.Parse(TextHelpers.RandomNumberString()),
                NumberOfPassengers = journeyRequest.People,
                PickUpLocation = journeyRequest.PickUpLocation,
                Status = nameof(Status.Created)
            };

            await _carPoolingDbContext.JourneyDetails.InsertOneAsync(journeyDetail);

            //map to dto
            JourneyResponse journeyResponse = JourneyMapper.MapToDto(journeyDetail);

            return new OperationResponse<JourneyResponse>(journeyResponse);
        }

        public async Task<OperationResponse<JourneyResponse>> EndJourneyAsync(string ID)
        {
            //check if journey exists for the specified group
            JourneyDetail journeyDetail = await _carPoolingDbContext.JourneyDetails
                                          .Find(journey => journey.GroupId == ID)
                                          .FirstOrDefaultAsync();

            if (journeyDetail == null)
            {
                return new OperationResponse<JourneyResponse>()
                                       .SetAsFailureResponse(OperationErrorDictionary.CarPoolingErrorMessage.EntityNotFound());
            }

            //update journey status for current journey
            journeyDetail.Status = nameof(Status.Ended);

            await _carPoolingDbContext.JourneyDetails.ReplaceOneAsync(journey => journey.GroupId == journeyDetail.GroupId, journeyDetail);

            //map to dto
            JourneyResponse journeyResponse = JourneyMapper.MapToDto(journeyDetail);

            return new OperationResponse<JourneyResponse>(journeyResponse);
        }

    }
}
