using FluentValidation;
using LabLinkBackend.DTO;

public class PatientUpsertDtoValidator : AbstractValidator<PatientUpsertDto>
{
    public PatientUpsertDtoValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("UserId is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Patient name is required.");

        RuleFor(x => x.Dob)
            .NotNull()
            .WithMessage("Date of birth is required.");

        RuleFor(x => x.Gender)
            .NotEmpty()
            .WithMessage("Gender is required.");

        RuleFor(x => x.ContactInfo)
            .NotEmpty()
            .WithMessage("Contact information is required.");


        When(x => x.IsCreate, () =>
                {
                    RuleFor(x => x.PatientId)
                        .Null()
                        .WithMessage("PatientId must NOT be provided when creating a patient.");
                });

    
        When(x => !x.IsCreate, () =>
        {
            RuleFor(x => x.PatientId)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("PatientId is required for update.");
        });





    }
}