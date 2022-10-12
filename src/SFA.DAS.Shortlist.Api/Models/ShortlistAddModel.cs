﻿using FluentValidation;

namespace SFA.DAS.Shortlist.Api.Models
{
    public class ShortlistAddModel
    {
        public Guid ShortlistUserId { get; set; }
        public int Ukprn { get; set; }
        public int Larscode { get; set; }
        public string LocationDescription { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public static implicit operator Application.Domain.Entities.Shortlist(ShortlistAddModel model)
            => new Application.Domain.Entities.Shortlist
            {
                Id = Guid.NewGuid(),
                ShortlistUserId = model.ShortlistUserId,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Ukprn = model.Ukprn,
                Larscode = model.Larscode,
                LocationDescription = model.LocationDescription
            };
    }

    public class ShortlistAddModelValidator : AbstractValidator<ShortlistAddModel>
    {
        public ShortlistAddModelValidator()
        {
            RuleFor(x => x.ShortlistUserId).NotEmpty();
            RuleFor(x => x.Ukprn).NotEmpty();
            RuleFor(x => x.Larscode).NotEmpty();
        }
    }
}