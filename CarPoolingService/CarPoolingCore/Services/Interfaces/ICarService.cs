using CarPoolingCore.Common;
using CarPoolingCore.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingCore.Services.Interfaces
{
    public interface ICarService
    {
        Task<OperationResponse<List<AvailableCarDto>>> UpdateAvailableCarAsync(List<AvailableCarRequest> availableCarRequests);
        Task<OperationResponse<GroupCarDto>> RetrieveGroupCarAsync(string ID);
    }
}
