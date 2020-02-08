
using BeFaster.Domain.Cqrs;
using FluentValidation;

namespace BeFaster.Domain.Cqrs.Validators
{
    public class HelloCommandValidator : AbstractValidator<HelloCommand>
    {
        public HelloCommandValidator()
        {
            RuleFor(x => x.Message)
                .NotNull()
                .WithErrorCode(ValidationErrors.MessageIsRequired.Code.ToString())
                .WithMessage(ValidationErrors.MessageIsRequired.Message);          
        }
    }
}

