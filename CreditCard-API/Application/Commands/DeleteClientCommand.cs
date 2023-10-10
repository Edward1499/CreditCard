using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class DeleteClientCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteClientCommand(int id)
        {
            Id = id;
        }
    }
}
