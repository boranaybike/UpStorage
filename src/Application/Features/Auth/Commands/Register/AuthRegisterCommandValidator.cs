using Application.Common.Interfaces;
using FluentValidation;
namespace Application.Features.Auth.Commands.Register
{
    public class AuthRegisterCommandValidator:AbstractValidator<AuthRegisterCommand>
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthRegisterCommandValidator(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

            RuleFor(x => x.Email).MustAsync(CheckIfUserExist)
                .WithMessage("There is already an user with given email.");
        }
        private async Task<bool> CheckIfUserExist(string email, CancellationToken cancellationToken)
        {
            var doesExist = await _authenticationService.CheckIfUserExist(email, cancellationToken);

            if (doesExist) return false;
            return true;
        }

    }
}
