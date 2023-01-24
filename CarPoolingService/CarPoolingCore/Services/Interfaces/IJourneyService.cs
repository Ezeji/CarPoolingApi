using CarPoolingCore.Common;
using CarPoolingCore.Dto;
using CarPoolingInfrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingCore.Services.Interfaces
{
    public interface IJourneyService
    {
        Task<OperationResponse<JourneyResponse>> CreateJourneyAsync(CreateJourneyRequest journeyRequest);
        Task<OperationResponse<JourneyResponse>> EndJourneyAsync(string ID);
    }
}
