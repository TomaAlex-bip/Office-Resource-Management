using Moq;
using Project3Api.Core;
using Project3Api.Core.Dtos;
using Project3Api.Core.Entities;
using Project3Api.Core.Services;
using Project3Api.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace Project3Api.UnitTests.ServiceTests
{
    public class AllocationServiceTests
    {
        private readonly IAllocationService _allocationService;

        private const string EXISTENT_USER = "User Existent";
        private readonly Guid existentUserId = Guid.NewGuid();
        private const string INEXISTENT_USER = "User Non Existent";

        private const string DESK_NAME = "A-9999";
        private readonly Guid deskId = Guid.NewGuid();

        public AllocationServiceTests()
        {
            // NOT WORKING!!! CANNOT OVERRIDE RETURN for Users from dbContext

            // Mock<DbSet<User>> usersDbSetMock = new();
            // usersDbSetMock.Setup(x => x.Add(new User
            // {
            //     Id = existentUserId,
            //     Username = EXISTENT_USER,
            //     Password = EXISTENT_USER,
            //     Role = 0,
            //     DeskAllocations = new List<DeskAllocation>()
            //     {
            //         new DeskAllocation()
            //         {
            //             Id = Guid.NewGuid(),
            //             DeskId = Guid.Empty,
            //             ReservedFrom = DateTime.Today.Date,
            //             ReservedUntil = DateTime.Today.Date,
            //             UserId = existentUserId
            //         }
            //     }
            // }));
            // 
            // Mock<ProjectDbContext> dbContextMock = new();
            // dbContextMock.Setup(x => x.Users).Returns(usersDbSetMock.Object);
            // 
            // UserRepository userRepository = new UserRepository(dbContextMock.Object);
            // 
            // Mock<IUnitOfWork> unitOfWorkMock = new();
            // unitOfWorkMock.Setup(x => x.UserRepository).Returns(userRepository);

            List<DeskAllocation> deskAllocations = new()
            {
                new DeskAllocation()
                {
                    Id = Guid.NewGuid(),
                    DeskId = deskId,
                    ReservedFrom = DateTime.Today.Date,
                    ReservedUntil = DateTime.Today.Date,
                    UserId = existentUserId
                }
            };

            Desk desk = new()
            {
                Id = deskId,
                Name = DESK_NAME,
                DeskAllocations = deskAllocations,
                GridPositionX = 0,
                GridPositionY = 0,
                Orientation = 0
            };

            Mock<IUnitOfWork> unitOfWorkMock = new();
            unitOfWorkMock.Setup(x => x.UserRepository
            .GetDeskAllocationsAsync(EXISTENT_USER))
                .ReturnsAsync(deskAllocations);

            unitOfWorkMock.Setup(x => x.UserRepository
            .GetDeskAllocationsAsync(INEXISTENT_USER))
                .ReturnsAsync((IEnumerable<DeskAllocation>?)null);

            unitOfWorkMock.Setup(x => x.DeskRepository
            .GetDeskAsync(deskId))
                .ReturnsAsync(desk);


            _allocationService = new AllocationService(unitOfWorkMock.Object);
        }

        [Theory]
        [InlineData(EXISTENT_USER, false)]
        [InlineData(INEXISTENT_USER, true)]
        public async void AllocationService_GetAllocatedDesksOfUser(string username, bool hasError)
        {
            List<DeskAllocationDto> deskAllocations = (List<DeskAllocationDto>)await _allocationService.GetDeskAllocationsForUser(username);

            bool hasAnyErrors = false;
            if(deskAllocations.Count > 0)
            {
                if(deskAllocations[0].ErrorMessage != null)
                {
                    hasAnyErrors = true;
                }
            }

            Assert.Equal(hasError, hasAnyErrors);
        }

    }
}
