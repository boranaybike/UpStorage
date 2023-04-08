using Application.Features.Addresses.Commands.Add;
using Application.Features.Addresses.Queries.GetAll;
using Application.Features.Cities.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ApiControllerBase
    {

        [HttpPost] async Task<IActionResult> AddAsync(AddressAddCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpGet] async Task<IActionResult> GetAllAsync(AddressGetAllQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await Mediator.Send(new AddressGetAllQuery(id, null)));
        }
    }
}
