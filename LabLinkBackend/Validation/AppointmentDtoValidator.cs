using FluentValidation;
using LabLinkBackend.DTO;

namespace LabLinkBackend.Validation
{
    public class AppointmentDtoValidator : AbstractValidator<AppointmentDto>
    {
        public AppointmentDtoValidator()
        {
            RuleFor(a => a.PatientId)
                .GreaterThan(0)
                .WithMessage("PatientId must be greater than 0.");

            RuleFor(a => a.Address)
                .NotNull()
                .NotEmpty()
                .WithMessage("Address is required and cannot be empty.");

            RuleFor(a => a.PhlebotomistId)
                .GreaterThan(0)
                .When(a => a.PhlebotomistId.HasValue)
                .WithMessage("PhlebotomistId must be greater than 0.");

            RuleFor(a => a.BookedDateTime)
                .Must(BeAValidFutureDate)
                .WithMessage("Appointment date must be a future date after today.");
        }

        private static bool BeAValidFutureDate(DateTime bookedDateTime)
        {
            return bookedDateTime.Date > DateTime.Today;
        }
    }
}
