using FluentValidation.TestHelper;
using SFA.DAS.Shortlist.Api.Models;

namespace SFA.DAS.Shortlist.Application.UnitTests
{
    [TestFixture]
    public class ShortlistAddModelValidatorTests
    {
        [TestCase("3fa85f64-5717-4562-b3fc-2c963f66afa6", true)]
        [TestCase("00000000-0000-0000-0000-000000000000", false)]
        public void Validate_ShortlistUserId_ShouldBeValid(string userId, bool isValid)
        {
            var sut = new ShortlistAddModelValidator();
            var model = new ShortlistAddModel { ShortlistUserId = Guid.Parse(userId) };
            var result = sut.TestValidate(model);
            if (isValid)
                result.ShouldNotHaveValidationErrorFor(x => x.ShortlistUserId);
            else
                result.ShouldHaveValidationErrorFor(x => x.ShortlistUserId);
        }

        [TestCase(10012002, true)]
        [TestCase(0, false)]
        public void Validate_Ukprn_ShouldBeValid(int ukprn, bool isValid)
        {
            var sut = new ShortlistAddModelValidator();
            var model = new ShortlistAddModel { Ukprn = ukprn };
            var result = sut.TestValidate(model);
            if (isValid)
                result.ShouldNotHaveValidationErrorFor(x => x.Ukprn);
            else
                result.ShouldHaveValidationErrorFor(x => x.Ukprn);
        }

        [TestCase(1, true)]
        [TestCase(0, false)]
        public void Validate_Larscode_ShouldBeValid(int larscode, bool isValid)
        {
            var sut = new ShortlistAddModelValidator();
            var model = new ShortlistAddModel { Larscode = larscode };
            var result = sut.TestValidate(model);
            if (isValid)
                result.ShouldNotHaveValidationErrorFor(x => x.Larscode);
            else
                result.ShouldHaveValidationErrorFor(x => x.Larscode);
        }
    }
}