using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project3Api.Core.Dtos;
using Project3Api.Core.Services;
using Project3Api.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Project3Api.Controllers
{
    [ApiController]
    [Authorize(Policy = "AdminApi")]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IDeskWriteService _deskWriteService;
        private readonly ILogService _logService;

        public AdminController(ILogger<AdminController> logger,
                               IDeskWriteService deskWriteService,
                               ILogService logService)
        {
            _logger = logger;
            _deskWriteService = deskWriteService;
            _logService = logService;
        }

        [HttpGet]
        [Route("logs")]
        public async Task<IActionResult> GetLogs([FromQuery][Required] int offset, 
                                                       [FromQuery] int size=20,
                                                       [FromQuery] string? filter=null,
                                                       [FromQuery] string? order=null,   // asc or desc
                                                       [FromQuery] string? orderBy="date",
                                                       [FromQuery] string? fromDate=null,
                                                       [FromQuery] string? untilDate=null)
        {
            var logsResponse = await _logService.GetLogs(offset, size, filter, order, orderBy ?? "date");

            if(logsResponse.LogsCount > 0 && logsResponse.Logs.Count() <= 0)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, logsResponse);
            }

            return StatusCode((int)HttpStatusCode.OK, logsResponse);
        }

        [HttpPost]
        [Route("desks")]
        public async Task<IActionResult> AddNewDesk([FromBody] DeskViewModel deskViewModel)
        {
            _logger.LogInformation("Called Admin - AddNewDesk Controller");
            DeskDto addedDesk = 
                await _deskWriteService.AddDesk(
                    deskViewModel.Name,
                    deskViewModel.GridPositionX,
                    deskViewModel.GridPositionY,
                    deskViewModel.Orientation);

            if(addedDesk.ErrorMessage is not null)
            {
                _logger.LogError($"Admin - AddNewDesk: Error: {addedDesk.ErrorMessage}");
                return StatusCode((int)HttpStatusCode.BadRequest, addedDesk);
            }

            _logger.LogInformation($"Admin - AddNewDesk: Added a new desk: {addedDesk.Name}");
            return StatusCode((int)HttpStatusCode.OK, addedDesk);
        }

        [HttpPut]
        [Route("desks/{deskName}")]
        public async Task<IActionResult> UpdateDesk([FromRoute]string deskName, 
                                                    [FromBody]DeskViewModel deskViewModel)
        {
            _logger.LogInformation("Called Admin - UpdateDesk Controller");
            DeskDto updatedDesk = 
                await _deskWriteService.UpdateDesk(
                    deskName,
                    deskViewModel.Name,
                    deskViewModel.GridPositionX,
                    deskViewModel.GridPositionY,
                    deskViewModel.Orientation);

            if (updatedDesk.ErrorMessage != null)
            {
                _logger.LogError($"Admin - UpdateDesk: Error: {updatedDesk.ErrorMessage}");
                return StatusCode((int)HttpStatusCode.BadRequest, updatedDesk);
            }

            _logger.LogInformation($"Admin - AddNewDesk: Updated desk: {updatedDesk.Name}");
            return StatusCode((int)HttpStatusCode.OK, updatedDesk);
        }

        [HttpDelete]
        [Route("desks/{deskName}")]
        public async Task<IActionResult> DeleteDesk([FromRoute] string deskName)
        {
            _logger.LogInformation("Called Admin - DeleteDesk Controller");
            DeskDto deletedDesk = await _deskWriteService.DeleteDesk(deskName);

            if (deletedDesk.ErrorMessage != null)
            {
                _logger.LogError($"Admin - AddNewDesk: Error: {deletedDesk.ErrorMessage}");
                return StatusCode((int)HttpStatusCode.BadRequest, deletedDesk);
            }

            _logger.LogInformation($"Admin - AddNewDesk: Deleted desk: {deletedDesk.Name}");
            return StatusCode((int)HttpStatusCode.OK, deletedDesk);
        }
    }
}
