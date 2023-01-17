
using AutoFixture;
using Project3Api.Validators;
using Project3Api.ViewModels;
using System;
using Xunit;

namespace Project3Api.UnitTests.ValidatorTests
{
    public class DeskReservationValidatorTests
    {
        private readonly DeskReservationValidator _validator;
        private readonly Fixture _fixture;

        public DeskReservationValidatorTests()
        {
            _validator = new();
            _fixture = new();
        }

        [Fact]
        public void DeskReservationValidator_ValidDeskReservation_ReturnsTrue()
        {
            DeskReservationViewModel deskReservationViewModel = new()
            {
                DeskName = "A-1111",
                ReservedFrom = DateTime.Today.Date,
                ReservedUntil = DateTime.Today.Date
            };

            //var test = _fixture.Create<DeskReservationViewModel>();

            var result = _validator.Validate(deskReservationViewModel);


            Assert.True(result.IsValid);
        }

        [Fact]
        public void DeskReservationValidator_DeskReservationDeskNameEmpty_ReturnsFalse()
        {
            DeskReservationViewModel deskReservationViewModel = new()
            {
                DeskName = " ",
                ReservedFrom = DateTime.Today.Date,
                ReservedUntil = DateTime.Today.Date
            };

            var result = _validator.Validate(deskReservationViewModel);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void DeskReservationValidator_DeskReservationDeskNameTooShort_ReturnsFalse()
        {
            DeskReservationViewModel deskReservationViewModel = new()
            {
                DeskName = "A",
                ReservedFrom = DateTime.Today.Date,
                ReservedUntil = DateTime.Today.Date
            };

            var result = _validator.Validate(deskReservationViewModel);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void DeskReservationValidator_DeskReservationDeskNameTooLong_ReturnsFalse()
        {
            DeskReservationViewModel deskReservationViewModel = new()
            {
                DeskName = "A-1234234234234234234234234234234",
                ReservedFrom = DateTime.Today.Date,
                ReservedUntil = DateTime.Today.Date
            };

            var result = _validator.Validate(deskReservationViewModel);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void DeskReservationValidator_DeskReservationReservationFromGreaterThanUntil_ReturnsFalse()
        {
            DeskReservationViewModel deskReservationViewModel = new()
            {
                DeskName = "A-1111",
                ReservedFrom = DateTime.Today.Date.AddDays(1),
                ReservedUntil = DateTime.Today.Date
            };

            var result = _validator.Validate(deskReservationViewModel);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void DeskReservationValidator_DeskReservationReservationFromLessThanToday_ReturnsFalse()
        {
            DeskReservationViewModel deskReservationViewModel = new()
            {
                DeskName = "A-1111",
                ReservedFrom = DateTime.Today.Date.AddDays(-1),
                ReservedUntil = DateTime.Today.Date
            };

            var result = _validator.Validate(deskReservationViewModel);

            Assert.False(result.IsValid);
        }

        

    }
}
