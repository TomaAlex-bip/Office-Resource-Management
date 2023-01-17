using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project3Api.Core.Dtos;
using Project3Api.Core.Services;
using System.Net;

namespace Project3Api.Controllers
{
    [ApiController]
    [Authorize(Policy = "ReadApi")]
    [Route("api/read")]
    public class ReadController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IDeskReadService _deskReadService;
        private readonly IRegistrationService _registrationService;
        private readonly ITokenReaderService _tokenReaderService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReadController(ILogger<UsersController> logger,
                              IDeskReadService deskReadService,
                              IHttpContextAccessor httpContextAccessor,
                              ITokenReaderService tokenReaderService,
                              IRegistrationService registrationService)
        {
            _logger = logger;
            _deskReadService = deskReadService;
            _registrationService = registrationService;
            _tokenReaderService = tokenReaderService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("desks")]
        public async Task<IEnumerable<DeskDto>> GetAllDesks()
        {
            _logger.LogInformation("Called Read - GetAllDesks Controller");
            return await _deskReadService.GetAllDesksAsync();
        }

    }
}
