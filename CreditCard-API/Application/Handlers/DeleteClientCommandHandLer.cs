using Application.Commands;
using Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class DeleteClientCommandHandLer : IRequestHandler<DeleteClientCommand>
    {
        private readonly IClientRepository _clientRepository;

        public DeleteClientCommandHandLer(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(request.Id);
            await _clientRepository.DeleteAsync(client);
        }
    }
}
