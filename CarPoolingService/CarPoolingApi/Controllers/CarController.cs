using CarPoolingCore.Common;
using CarPoolingCore.Dto;
using CarPoolingCore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolingApi.Controllers
{
    [Route("/")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService
                                     ?? throw new ArgumentNullException(nameof(carService));
        }

        /// <summary>
        /// Update available car
        /// </summary>
        /// <response code="200">When the list is registered correctly</response>
        /// <response code="400">When there is a failure in the request format, expected headers, or the payload can't be unmarshalled</response>
        /// <response code="500">Server error</response>
        [ProducesResponseType(typeof(List<AvailableCarDto>), 200)]
        [HttpPut("cars")]
        public async Task<IActionResult> UpdateAvailableCarAsync([FromBody] List<AvailableCarRequest> availableCarRequests)
        {
            if (availableCarRequests == null || !ModelState.IsValid)
            {
                return BadRequest(OperationErrorDictionary.CarPoolingErrorMessage.Failed());
            }

            OperationResponse<List<AvailableCarDto>> operationResponse = await _carService.UpdateAvailableCarAsync(availableCarRequests);

            if (!operationResponse.CompletedWithSuccess)
            {
                return BadRequest(operationResponse.OperationError);
            }

            return Ok(operationResponse.Result);
        }

        /// <summary>
        /// Locate group car
        /// </summary>
        /// <response code="200">When the car assigned to the group is retrieved successfully</response>
        /// <response code="204">When the group is waiting to be assigned to a car</response>
        /// <response code="400">When there is a failure in the request format, expected headers, or the payload can't be unmarshalled</response>
        /// <response code="404">When the group is not to be found</response>
        /// <response code="500">Server error</response>
        [ProducesResponseType(typeof(GroupCarDto), 200)]
        [HttpPost("locate")]
        public async Task<IActionResult> LocateGroupCarAsync([FromForm] string ID)
        {
            if (string.IsNullOrEmpty(ID) || !ModelState.IsValid)
            {
                return BadRequest(OperationErrorDictionary.CarPoolingErrorMessage.Failed());
            }

            OperationResponse<GroupCarDto> operationResponse = await _carService.RetrieveGroupCarAsync(ID);

            if (operationResponse.OperationError == OperationErrorDictionary.CarPoolingErrorMessage.EntityNotFound())
            {
                return NotFound(null);
            }

            return Ok(operationResponse.Result);
        }

    }
}
