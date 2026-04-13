using FluentValidation;
using JsonWebToken.DTO;

 
namespace LabLinkBackend.Validation
{
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required")
                .NotNull().WithMessage("Email cannot be Null !");
 
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required")
                .NotNull().WithMessage("Password cannot be Null !");
        }
    }
}
