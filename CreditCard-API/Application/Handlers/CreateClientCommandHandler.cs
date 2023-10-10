using Application.Commands;
using AutoMapper;
using Core.Entities;
using Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly IMapper _mapper;
        public CreateClientCommandHandler(IClientRepository clientRepository,
            ICreditCardRepository creditCardRepository,
            IMapper mapper)
        {
            _clientRepository = clientRepository;
            _creditCardRepository = creditCardRepository;
            _mapper = mapper;
        }

        public async Task Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            request.CreditCards.ForEach(item =>
            {
                bool exists = _creditCardRepository.AnyAsync(x => x.Number == item.Number && x.Id != item.Id)
                    .Result;
                if (exists)
                {
                    throw new ApplicationException("El numero de tarjeta ya se encuentra registrado!");
                }
            });
            await _clientRepository.AddAsync(_mapper.Map<Client>(request));
        }
    }
}
