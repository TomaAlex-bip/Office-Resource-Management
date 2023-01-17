using FluentValidation;
using Project3Api.Core.Configuration;
using Project3Api.ViewModels;

namespace Project3Api.Validators
{
    public class DeskReservationValidator : AbstractValidator<DeskReservationViewModel>
    {
        public DeskReservationValidator()
        {
            RuleFor(x => x.DeskName)
                .NotEmpty()
                .Length(EntityHelperConstants.DESK_NAME_MIN_LENGTH,
                        EntityHelperConstants.DESK_NAME_MAX_LENGTH);

            RuleFor(x => x.ReservedFrom)
                .GreaterThanOrEqualTo(DateTime.Today.Date)
                .LessThanOrEqualTo(x => x.ReservedUntil)
                .NotEmpty();

            RuleFor(x => x.ReservedUntil)
                .GreaterThanOrEqualTo(x => x.ReservedFrom)
                .NotEmpty();
        }
    }
}
