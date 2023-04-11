using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Cities.Commands.Add
{
    public class CityAddCommandValidator:AbstractValidator<CityAddCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public CityAddCommandValidator(IApplicationDbContext applicationDbContext) 
        {
            _applicationDbContext = applicationDbContext;
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.CountryId).NotEmpty();
            RuleFor(x => x.CountryId).MustAsync(DoesCountryExistsAsync)
                .WithMessage("The selected country does not exist"); //true ise validationdan geçti


            //RuleFor(x => x.CountryId).MustAsync(IsCountryIdsListValid)
            //                    .WithMessage("The selected country does not exist"); //true ise validationdan geçti

            RuleFor(x => x.Name).MustAsync((command, name, cancellationToken) =>
            {
                return _applicationDbContext.Cities.AllAsync(x => x.Name.ToLower() == name.ToLower(), cancellationToken);
            })
                .WithMessage("The selected country does not exist");




        }

        private Task<bool> DoesCountryExistsAsync(int countryId, CancellationToken cancellationToken)
        {
            return _applicationDbContext.Countries.AllAsync(x => x.Id == countryId, cancellationToken);
        }
        //private bool IsCountryIdsListValid(List<Guid> countryIds)
        //{
        //    if(countryIds is null || !countryIds.Any()|| countryIds.Count < 2)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
    }
}
