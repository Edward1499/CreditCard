using Application.Commands;
using Application.Responses;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class ClientMappingProfile : Profile
    {
        public ClientMappingProfile()
        {
            CreateMap<Client, CreateClientCommand>().ReverseMap();
            CreateMap<Client, ClientResponse>().ReverseMap();
            CreateMap<Client, UpdateClientCommand>().ReverseMap();
        }
    }
}
