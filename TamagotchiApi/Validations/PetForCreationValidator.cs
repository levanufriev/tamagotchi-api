using Entities.DataTransferObjects;
using FluentValidation;

namespace TamagotchiApi.Validations
{
    public class PetForCreationValidator : AbstractValidator<PetForCreationDto>
    {
        public PetForCreationValidator()
        {
            var msg = "Mistake in the field {PropertyName}: value {PropertyValue}";

            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Age).GreaterThan(-1).WithMessage(msg);
            RuleFor(x => x.ThirstyLevel).GreaterThan(-1).LessThan(6).WithMessage(msg);
            RuleFor(x => x.HungerLevel).GreaterThan(-1).LessThan(6).WithMessage(msg);
        }
    }
}
