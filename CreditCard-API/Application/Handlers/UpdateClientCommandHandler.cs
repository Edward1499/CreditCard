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
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly IMapper _mapper;
        public UpdateClientCommandHandler(IClientRepository clientRepository,
            ICreditCardRepository creditCardRepository,
            IMapper mapper)
        {
            _clientRepository = clientRepository;
            _creditCardRepository = creditCardRepository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            request.CreditCards.ForEach(item =>
            {
                bool exists = _creditCardRepository.AnyAsync(x => x.Number == item.Number && x.ClientId != request.Id)
                    .Result;
                if (exists)
                {
                    throw new ApplicationException("El numero de tarjeta ya se encuentra registrado!");
                }
            });
            var client = await _clientRepository.GetByIdAsync(request.Id);
            await _creditCardRepository.DeleteAsync(await _creditCardRepository.GetAllAsync(x => x.ClientId == client.Id));
            await _clientRepository.UpdateAsync(_mapper.Map<Client>(request));
            await _creditCardRepository.AddRangeAsync(request.CreditCards);
        }
    }
}
