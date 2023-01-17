using FluentValidation.Results;
using Project3Api.Validators;
using Project3Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Project3Api.UnitTests.ValidatorTests
{
    public class UserValidatorTests
    {
        private readonly UserValidator _validator;

        public UserValidatorTests()
        {
            _validator = new UserValidator();
        }

        [Theory]
        [InlineData("Gigel Bobel", "P4R0la_f0art3~$mek3r499999", true)]
        [InlineData("john_bobel", "P4R0la_f0art3~$mek3r499999", true)]
        [InlineData("john_bobel", "parolasimpladarlunga", true)]
        [InlineData("john_bobel", "1234", false)]
        [InlineData(" ", "P4R0la_f0art3~$mek3r499999", false)]
        [InlineData(" ", "", false)]
        [InlineData("bob", "abc", false)]
        public void UserValidator_TheoryTest(string username, string password, bool expectedResult)
        {
            UserViewModel user = new()
            {
                Username = username,
                Password = password
            };

            ValidationResult result = _validator.Validate(user);

            Assert.Equal(result.IsValid, expectedResult);
        }
    }
}
