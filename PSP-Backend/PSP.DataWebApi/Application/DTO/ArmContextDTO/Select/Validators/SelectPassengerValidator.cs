using FluentValidation;

namespace Application.DTO.ArmContextDTO.Select.Validators;

public class SelectPassengerValidator : AbstractValidator<SelectPassengerRequestDTO>
{
    public SelectPassengerValidator()
    {
        RuleFor(x => x.Surname)
            .MaximumLength(40)
            .WithMessage("Длинная фамилия")
            .WithErrorCode("PPC-000403");
        
        RuleFor(x => x.Name)
            .MaximumLength(40)
            .WithMessage("Длинное имя")
            .WithErrorCode("PPC-000403");
        
        RuleFor(x => x.Patronymic)
            .MaximumLength(40)
            .WithMessage("Длинное отчество")
            .WithErrorCode("PPC-000403");
        
        RuleFor(x => x.Birthdate.Year)
            .LessThan(9999)
            .GreaterThan(1000)
            .WithMessage("Неверный формат года")
            .WithErrorCode("PPC-000403");
        
        RuleFor(x => x.Gender)
            .Must(g => g is "M" or "F")
            .WithMessage("Неверный пол (M или F)")
            .WithErrorCode("PPC-000403");
        
        RuleFor(x => x.DocumentType)
            .MaximumLength(2)
            .WithMessage("Длинный тип документа")
            .WithErrorCode("PPC-000403");
        
        RuleFor(x => x.DocumentNumber)
            .MaximumLength(20)
            .WithMessage("Длинный номер документа")
            .WithErrorCode("PPC-000403");
        
        RuleFor(x => x.Snils)
            .Length(11)
            .WithMessage("Неверный номер снилса")
            .WithErrorCode("PPC-000403");
    }
}