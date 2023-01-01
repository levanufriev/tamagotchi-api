using Entities.DataTransferObjects;
using FluentValidation;

namespace TamagotchiApi.Validations
{
    public class UserForRegistrationValidator : AbstractValidator<UserForRegistrationDto>
    {
        public UserForRegistrationValidator()
        {
            string notNullOrEmptyMessage = "{PropertyName} field must be not null or empty.";
            RuleFor(u => u.FirstName).NotNull().NotEmpty().WithMessage(notNullOrEmptyMessage);
            RuleFor(u => u.LastName).NotNull().NotEmpty().WithMessage(notNullOrEmptyMessage);
            RuleFor(u => u).Must(ValidPassword).WithMessage("Password confirmed incorrectly.");
        }

        private bool ValidPassword(UserForRegistrationDto user)
        {
            return user.Password == user.ConfirmPassword;
        }
    }
}
