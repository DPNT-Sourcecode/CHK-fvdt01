
using BeFaster.Domain.Cqrs;
using FluentValidation;

namespace BeFaster.Domain.Cqrs.Validators
{
    public class CalculateSumCommandValidator : AbstractValidator<CalculateSumCommand>
    {
        public CalculateSumCommandValidator()
        {
            RuleFor(x => x.Param1)
                .NotEmpty()
                .WithErrorCode(ValidationErrors.Param1IsRequired.Code.ToString())
                .WithMessage(ValidationErrors.Param1IsRequired.Message);

            RuleFor(x => x.Param1)
                .NotEmpty()
                .InclusiveBetween(0,100)
                .WithErrorCode(ValidationErrors.Param1IsInvalid.Code.ToString())
                .WithMessage(ValidationErrors.Param1IsInvalid.Message);

            RuleFor(x => x.Param2)
                .NotEmpty()
                .WithErrorCode(ValidationErrors.Param2IsRequired.Code.ToString())
                .WithMessage(ValidationErrors.Param2IsRequired.Message);

            RuleFor(x => x.Param2)
                .NotEmpty()
                .InclusiveBetween(0, 100)
                .WithErrorCode(ValidationErrors.Param2IsInvalid.Code.ToString())
                .WithMessage(ValidationErrors.Param2IsInvalid.Message);
        }
    }
}
