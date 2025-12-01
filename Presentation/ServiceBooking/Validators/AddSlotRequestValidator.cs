using FluentValidation;
using ServiceBooking.WebApi.Models.Requests;

namespace ServiceBooking.WebApi.Validators
{
    public class AddSlotRequestValidator : AbstractValidator<AddSlotRequest>
    {
        public AddSlotRequestValidator()
        {
            RuleFor(x => x.ServiceId)
                .NotEmpty()
                .WithMessage("ServiceId не заполнен.");

            RuleFor(x => x.SlotDateTime)
                .NotEmpty()
                .WithMessage("Дата и время слота обязательны.")
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Дата и время слота должны быть в будущем.");
        }
    }
}
