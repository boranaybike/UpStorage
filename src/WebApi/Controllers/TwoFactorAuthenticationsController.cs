using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwoFactorAuthenticationsController : ControllerBase
    {
        private readonly ITwoFactorService _twoFactorService;
        public TwoFactorAuthenticationsController(ITwoFactorService twoFactorService)
        {
            _twoFactorService = twoFactorService;
        }

        [HttpPost("GenerateQR")]
        public IActionResult GenerateQR(string email)
        {
            var twoFactorDto = _twoFactorService.Generate(email);
            return File(twoFactorDto.QrCodeImage, "image/png");
        }
        
        [HttpPost("Generate")]
        public IActionResult Generate(string email)
        {
            var twoFactorDto = _twoFactorService.Generate(email);
            return Ok(twoFactorDto.Key);
        }

        [HttpPost("Validate")]
        public IActionResult Validate(string userCode)
        {
            var isValid = _twoFactorService.Validate(userCode);

            if (isValid) return Ok("You're Authenticated!");

            return BadRequest("Your code is not valid. Please try again for god sake.");
        }
    }
}
