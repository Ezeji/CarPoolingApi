using CarPoolingCore.Common;
using CarPoolingCore.Dto;
using CarPoolingCore.Services.Interfaces;
using CarPoolingInfrastructure.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolingApi.Controllers
{
    [Route("/")]
    [ApiController]
    public class JourneyController : ControllerBase
    {
        private readonly IJourneyService _journeyService;

        public JourneyController(IJourneyService journeyService)
        {
            _journeyService = journeyService
                                     ?? throw new ArgumentNullException(nameof(journeyService));
        }

        /// <summary>
        /// Create new journey
        /// </summary>
        /// <response code="200">When the group is registered correctly</response>
        /// <response code="400">When there is a failure in the request format, expected headers, or the payload can't be unmarshalled</response>
        /// <response code="500">Server error</response>
        [ProducesResponseType(typeof(JourneyResponse), 200)]
        [HttpPost("journey")]
        public async Task<IActionResult> PostJourneyAsync([FromBody] CreateJourneyRequest journeyRequest)
        {
            if (journeyRequest == null || !ModelState.IsValid)
            {
                return BadRequest(OperationErrorDictionary.CarPoolingErrorMessage.Failed());
            }

            OperationResponse<JourneyResponse> operationResponse = await _journeyService.CreateJourneyAsync(journeyRequest);

            if (!operationResponse.CompletedWithSuccess)
            {
                return BadRequest(operationResponse.OperationError);
            }

            return Ok();
        }

        /// <summary>
        /// End existing journey
        /// </summary>
        /// <response code="200">When the group is unregistered correctly</response>
        /// <response code="400">When there is a failure in the request format, expected headers, or the payload can't be unmarshalled</response>
        /// <response code="404">When the group is not to be found</response>
        /// <response code="500">Server error</response>
        [ProducesResponseType(typeof(JourneyResponse), 200)]
        [HttpPost("dropoff")]
        public async Task<IActionResult> EndJourneyAsync([FromForm] string ID)
        {
            if (string.IsNullOrEmpty(ID) || !ModelState.IsValid)
            {
                return BadRequest(OperationErrorDictionary.CarPoolingErrorMessage.Failed());
            }

            OperationResponse<JourneyResponse> operationResponse = await _journeyService.EndJourneyAsync(ID);

            if (operationResponse.OperationError == OperationErrorDictionary.CarPoolingErrorMessage.EntityNotFound())
            {
                return NotFound(null);
            }

            return Ok();
        }

    }
}
