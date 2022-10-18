using FluentAssertions;
using SFA.DAS.Shortlist.Api.Models;

namespace SFA.DAS.Shortlist.Api.UnitTests
{
    [TestFixture]
    public class ShortlistAddModelTests
    {
        [Test]
        public void Operator_ConvertsToEntity()
        {
            var model = new ShortlistAddModel()
            {
                ShortlistUserId = Guid.NewGuid(),
                Ukprn = 10012002,
                Larscode = 1,
                Latitude = 10,
                Longitude = 20,
                LocationDescription = Guid.NewGuid().ToString()
            };

            Application.Domain.Entities.Shortlist shortlist = model;

            shortlist.Should().BeEquivalentTo(model, options => 
            {
                options.ExcludingMissingMembers();
                return options;
            });
        }
    }
}
