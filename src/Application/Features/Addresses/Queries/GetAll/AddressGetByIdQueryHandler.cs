using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.Addresses.Queries.GetAll
{
    public class AddressGetByIdQueryHandler : IRequestHandler<AddressGetAllQuery, List<AddressGetAllDto>>
    {

        private readonly IApplicationDbContext _applicationDbContext;

        public AddressGetAllQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public Task<List<AddressGetAllDto>> Handle(AddressGetAllQuery request, CancellationToken cancellationToken)
        {
            
        }
    }
}
