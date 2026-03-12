using AbpPractice.Products.Dto;
using FluentValidation;

namespace AbpPractice.Products.Validation;

/// <summary>
/// Validates incoming product update requests.
/// </summary>
public class UpdateProductInputValidator : AbstractValidator<UpdateProductInput>
{
    public UpdateProductInputValidator()
    {
        RuleFor(input => input.Id)
            .GreaterThan(0);

        RuleFor(input => input.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(input => input.Description)
            .MaximumLength(500);

        RuleFor(input => input.Price)
            .GreaterThanOrEqualTo(0);
    }
}