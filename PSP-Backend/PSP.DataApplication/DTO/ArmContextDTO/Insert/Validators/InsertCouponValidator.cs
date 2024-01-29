using FluentValidation;

namespace PSP.DataApplication.DTO.ArmContextDTO.Insert.Validators;

public class InsertCouponValidator : AbstractValidator<InsertCouponDTO>
{
    public InsertCouponValidator()
    {
        RuleFor(x => x.AirlineCode)
            .MaximumLength(2)
            .WithMessage("Неверный формат кода авиакомпании")
            .WithErrorCode("PPC-000403");
            
        RuleFor(x => x.PnrCode)
            .MaximumLength(10)
            .WithMessage("Неверный формат года")
            .WithErrorCode("PPC-000403");
    }
}