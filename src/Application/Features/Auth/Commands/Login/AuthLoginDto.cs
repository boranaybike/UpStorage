using Domain.Common;
using MediatR;
namespace Application.Features.Auth.Commands.Login
{
    public class AuthLoginDto : IRequest<Response<AuthLoginDto>>
    {
        public string AccessToken {get;set;}
        public DateTime Expires {get;set;}

        public AuthLoginDto(string accessToken, DateTime expires)
        {
            AccessToken = accessToken;
            Expires = expires;
        }

    }
}
