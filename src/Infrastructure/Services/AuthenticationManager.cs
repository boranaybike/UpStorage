﻿using Application.Common.Interfaces;
using Application.Common.Models.Auth;
using Application.Features.Auth.Commands.Login;
using Domain.Identity;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class AuthenticationManager : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtService _jwtService;

        public AuthenticationManager(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        public async Task<string> CreateUserAsync(CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            var user = createUserDto.MapToUser();

            var identityResult = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!identityResult.Succeeded)
            {
                var failures = identityResult.Errors
                    .Select(x => new ValidationFailure(x.Code, x.Description));

                throw new ValidationException(failures);
            }
            var jwtDto = _jwtService.Generate(user.Id, user.Email, user.FirstName, user.LastName);

            return user.Id;

        }

        public async Task<string> GenerateEmailActivationTokenAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId);

            return await _userManager.GenerateEmailConfirmationTokenAsync(user);

        }

        public Task<bool> CheckIfUserExist(string email, CancellationToken cancellationToken)
        {
            return _userManager.Users.AnyAsync(x => x.Email == email, cancellationToken);
        }

        public async Task<JwtDto> LoginAsync(AuthLoginRequest authLoginRequest, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(authLoginRequest.Email);
            var loginResult = await _signInManager.PasswordSignInAsync(user,authLoginRequest.Password, false,false);
            if(!loginResult.Succeeded)
            {
                throw new ValidationException(CreateValidationFailure);
            }
            return _jwtService.Generate(user.Id, user.Email, user.FirstName,user.LastName);
        }
        private List<ValidationFailure> CreateValidationFailure => new List<ValidationFailure>()
        {
            new ValidationFailure("Email & Password", "Your Email or Password is incorrect.")   
        };
    }
}