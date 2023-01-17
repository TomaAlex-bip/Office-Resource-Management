using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project3Api.Core.Dtos;
using Project3Api.Core.Services;
using Project3Api.Services.LogMessages;
using Project3Api.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Project3Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IDeskWriteService _deskWriteService;
        private readonly IRegistrationService _registrationService;
        private readonly IAllocationService _allocationService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ITokenReaderService _tokenReaderService;
        private readonly ILogService _logService;

        public UsersController(ILogger<UsersController> logger,
                               IHttpContextAccessor contextAccessor,
                               IDeskWriteService deskWriteService,
                               IRegistrationService registrationService,
                               IAllocationService allocationService,
                               ITokenReaderService tokenReaderService,
                               ILogService logService)
        {
            _logger = logger;
            _deskWriteService = deskWriteService;
            _registrationService = registrationService;
            _allocationService = allocationService;
            _contextAccessor = contextAccessor;
            _tokenReaderService = tokenReaderService;
            _logService = logService;
        }

        [HttpGet]
        [Authorize(Policy = "UsersApi")]
        [Route("desks/allocations")]
        public async Task<IEnumerable<DeskAllocationDto>> GetUserDeskAllocation()
        {
            HttpContext? context = _contextAccessor.HttpContext;
            if(context == null)
            {
                return new List<DeskAllocationDto>();
            }

            string token = context.Request.Headers.Authorization;
            Guid userId = _tokenReaderService.GetUserId(token);

            return await _allocationService.GetDeskAllocationsForUser(userId);
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> RegisterUser(UserViewModel userViewModel)
        {
            UserDto userDto = await _registrationService
                .RegisterUserAsync(userViewModel.Username, 
                                   userViewModel.Password);

            if (userDto.ErrorMessage != null)
            {
                await _logService.LogAsync(LogMessageHelper
                    .GetRegistrationLogMessage(userViewModel.Username, userDto.ErrorMessage));

                return StatusCode((int)HttpStatusCode.BadRequest, userDto);
            }

            await _logService.LogAsync(LogMessageHelper
                    .GetRegistrationLogMessage(userViewModel.Username));

            return StatusCode((int)HttpStatusCode.OK, userDto);
        }

        [HttpPost]
        [Authorize(Policy = "UsersApi")]
        [Route("desks/allocations")]
        public async Task<IActionResult> MakeDeskReservation([FromBody]DeskReservationViewModel deskReservation)
        {
            HttpContext? context = _contextAccessor.HttpContext;
            if (context == null)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "Token not provided!");
            }

            string token = context.Request.Headers.Authorization;
            Guid userId = _tokenReaderService.GetUserId(token);

            DeskAllocationDto allocationDto = await _deskWriteService
                .MakeDeskReservation(userId, deskReservation.DeskName,
                                     deskReservation.ReservedFrom, deskReservation.ReservedUntil);

            if (allocationDto.ErrorMessage != null)
            {
                await _logService.LogAsync(LogMessageHelper
                    .GetDeskReservationLogMessage(userId.ToString(), deskReservation.DeskName, allocationDto.ErrorMessage));

                return StatusCode((int)HttpStatusCode.BadRequest, allocationDto);
            }

            await _logService.LogAsync(LogMessageHelper
                .GetDeskReservationLogMessage(userId.ToString(), deskReservation.DeskName));

            return StatusCode((int)HttpStatusCode.OK, allocationDto);
        }

    }
}
