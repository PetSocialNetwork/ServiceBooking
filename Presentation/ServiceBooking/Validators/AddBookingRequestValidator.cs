using FluentValidation;
using ServiceBooking.WebApi.Models.Requests;

namespace ServiceBooking.WebApi.Validators
{
    public class AddBookingRequestValidator : AbstractValidator<AddBookingRequest>
    {
        public AddBookingRequestValidator()
        {
            RuleFor(x => x.ProfileId)
                .NotEmpty()
                .WithMessage("ProfileId не заполнен.");

            RuleFor(x => x.ServiceId)
                .NotEmpty()
                .WithMessage("ServiceId не заполнен.");

            RuleFor(x => x.SlotId)
                .NotEmpty()
                .WithMessage("SlotId не заполнен.");
        }
    }
}
