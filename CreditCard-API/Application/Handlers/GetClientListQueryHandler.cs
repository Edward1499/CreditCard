using Application.Queries;
using Application.Responses;
using AutoMapper;
using Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class GetClientListQueryHandler : IRequestHandler<GetClientListQuery, List<ClientResponse>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public GetClientListQueryHandler(IClientRepository clientRepository,
            IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<List<ClientResponse>> Handle(GetClientListQuery request, CancellationToken cancellationToken)
        {
            var clientList = await _clientRepository.GetAllAsync();
            return _mapper.Map<List<ClientResponse>>(clientList);
        }
    }
}
