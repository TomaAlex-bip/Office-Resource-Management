using FluentValidation;
using Project3Api.Core.Configuration;
using Project3Api.ViewModels;

namespace Project3Api.Validators
{
    public class DeskValidator : AbstractValidator<DeskViewModel>
    {
        public DeskValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(EntityHelperConstants.DESK_NAME_MIN_LENGTH,
                        EntityHelperConstants.DESK_NAME_MAX_LENGTH);

            RuleFor(x => x.GridPositionX)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.GridPositionY)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Orientation)
                .InclusiveBetween(EntityHelperConstants.DESK_ORIENTATION_MIN,
                                  EntityHelperConstants.DESK_ORIENTATION_MAX);

        }
    }
}
