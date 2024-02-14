using Cards.Core.Models;
using FluentValidation;

namespace Cards.Core.Validations
{
    public class CreateCardValidator : AbstractValidator<CreateCardDto>
    {
        public CreateCardValidator()
        {
            RuleFor(card => card.Name)
           .NotEmpty().NotNull()
           .WithMessage("Name is required.");
            // Color, if provided, should conform to a "6 alphanumeric characters prefixed with a #"
            RuleFor(card => card.Color)
                .Matches("^#[0-9A-Fa-f]{6}$")
                .When(x => !string.IsNullOrEmpty(x.Color))
                .WithMessage("Color must be 6 alphanumeric characters prefixed with #.");
        }
    }
}
