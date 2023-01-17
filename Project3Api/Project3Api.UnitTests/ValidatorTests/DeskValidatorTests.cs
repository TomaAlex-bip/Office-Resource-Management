using FluentValidation.Results;
using Project3Api.Validators;
using Project3Api.ViewModels;
using Xunit;

namespace Project3Api.UnitTests.ValidatorTests
{
    public class DeskValidatorTests
    {
        private readonly DeskValidator _validator;

        public DeskValidatorTests()
        {
            _validator = new DeskValidator();
        }

        [Theory]
        [InlineData("A-1111", 0, 0, 0, true)]
        [InlineData("A-1111", 0, 0, 1, true)]
        [InlineData("A-1111", 4, 5, 0, true)]
        [InlineData("A-1111", 4, 5, 2, true)]
        [InlineData("A-0031", 4, 5, 1, true)]
        [InlineData("A-1111", -1, 0, 0, false)]
        [InlineData("A-1111", 0, -1, 0, false)]
        [InlineData("A-1111", -1, -1, 0, false)]
        [InlineData("", 0, 0, 0, false)]
        [InlineData("   ", 0, 0, 0, false)]
        [InlineData("A-", 0, 0, 0, false)]
        [InlineData("A-1111111111111111111111111111111111111", 0, 0, 0, false)]
        [InlineData("A", -10, -3, -1, false)]
        [InlineData("A-1111", 0, 0, 4, false)]
        [InlineData("A-11", 4, 5, 89, false)]
        public void DeskValidator_TheoryTest(string deskName, int gridPosX, int gridPosY, int orientation, bool expectedResult)
        {
            DeskViewModel deskViewModel = new()
            {
                Name = deskName,
                GridPositionX = gridPosX,
                GridPositionY = gridPosY,
                Orientation = orientation
            };

            ValidationResult result = _validator.Validate(deskViewModel);

            Assert.Equal(result.IsValid, expectedResult);
        }
    }
}
