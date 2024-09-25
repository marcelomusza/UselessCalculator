using FluentValidation;
using UselessCalculator.Api.Dtos;

namespace UselessCalculator.Api.Validators;

public class CalculationRequestDtoValidator : AbstractValidator<CalculationRequestDto>
{
    public CalculationRequestDtoValidator()
    {
        RuleFor(x => x.Num1)
            .NotEmpty()
            .WithMessage("Num1 is required...");

        RuleFor(x => x.Num2)
            .NotEmpty()
            .WithMessage("Num2 is required...");

    }
}
