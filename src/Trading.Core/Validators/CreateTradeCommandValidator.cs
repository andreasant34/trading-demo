using FluentValidation;
using Trading.Core.Commands;

namespace Trading.Core.Validators
{
    public class CreateTradeCommandValidator:AbstractValidator<CreateTradeCommand>
    {
        public CreateTradeCommandValidator()
        {
            RuleFor(x => x.TransactionType)
                .IsInEnum()
                .WithMessage("Invalid TransactionType");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0");

            RuleFor(x => x.CurrencyCode)
                .Length(3)
                .WithMessage("Invalid CurrencyCode");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0");

            RuleFor(x => x.TotalAmount)
                .Must((cmd, x) => x == cmd.Price * cmd.Quantity)
                .WithMessage("TotalAmount does not add up");
        }
    }
}
