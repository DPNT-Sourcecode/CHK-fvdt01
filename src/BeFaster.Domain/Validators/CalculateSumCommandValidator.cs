
using BeFaster.Domain.Cqrs;
using FluentValidation;

namespace BeFaster.Domain.Cqrs.Validators
{
    public class CalculateSumCommandValidator : AbstractValidator<CalculateSumCommand>
    {
        public CalculateSumCommandValidator()
        {
            RuleFor(x => x.Input1).NotEmpty()
                                .WithErrorCode(ValidationErrors.Input1IsRequired.Code.ToString())
                                .WithMessage(ValidationErrors.Input1IsRequired.Message);

            RuleFor(x => x.Input2).NotEmpty()
                               .WithErrorCode(ValidationErrors.Input2IsRequired.Code.ToString())
                               .WithMessage(ValidationErrors.Input2IsRequired.Message);
        }
    }
}
