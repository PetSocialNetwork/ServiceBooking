using FluentValidation;
using ServiceBooking.WebApi.Models.Requests;

namespace ServiceBooking.WebApi.Validators
{
    public class UpdateBookingStatusRequestValidator : AbstractValidator<UpdateBookingStatusRequest>
    {
        public UpdateBookingStatusRequestValidator()
        {
            RuleFor(x => x.BookingId)
            .NotEmpty()
            .WithMessage("BookingId не заполнен.");

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("Status не заполнен.");

        }
    }
}
