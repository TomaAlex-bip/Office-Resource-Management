using Moq;
using Project3Api.Core;
using Project3Api.Core.Dtos;
using Project3Api.Core.Entities;
using Project3Api.Core.Services;
using Project3Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Project3Api.UnitTests.ServiceTests
{
    public class RegistrationServiceTests
    {
        private readonly IRegistrationService _registrationService;

        private const string EXISTENT_USER = "User Existent";
        private const string INEXISTENT_USER = "User Non Existent";

        public RegistrationServiceTests()
        {
            User user = new()
            {
                Id = Guid.NewGuid(),
                Username = EXISTENT_USER,
                Password = EXISTENT_USER,
                Role = 0,
                DeskAllocations = new List<DeskAllocation>(),
                ResourceAllocations = new List<ResourceAllocation>()
            };

            Mock<IUnitOfWork> unitOfWorkMock = new();
            unitOfWorkMock.Setup(x => x.UserRepository
            .GetUserAsync(EXISTENT_USER))
                .ReturnsAsync(user);

            unitOfWorkMock.Setup(x => x.UserRepository
            .GetUserAsync(INEXISTENT_USER))
                .ReturnsAsync((User?)null);

            _registrationService = new RegistrationService(unitOfWorkMock.Object);
        }

        [Theory]
        [InlineData(EXISTENT_USER, true)]
        [InlineData(INEXISTENT_USER, false)]
        public async void RegistrationService_RegisterUser(string username, bool isDuplicate)
        {
            UserDto response = await _registrationService.RegisterUserAsync(username, username);

            bool hasAnyErrors = false;
            if(response.ErrorMessage != null)
            {
                hasAnyErrors = true;
            }

            Assert.Equal(hasAnyErrors, isDuplicate);
        }
    }
}
