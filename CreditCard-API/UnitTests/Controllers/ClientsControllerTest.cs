using Application.Queries;
using Application.Responses;
using Castle.Components.DictionaryAdapter.Xml;
using CreditCard_API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Controllers
{
    public class ClientsControllerTest
    {
        private Mock<IMediator> _mediator;

        [Fact]
        public void GetAll()
        {
            _mediator = new Mock<IMediator>();
            var controller = new ClientsController(_mediator.Object);

            var response = controller.GetAll().Result;

            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response as OkObjectResult);
        }

        [Fact]
        public async Task GetById()
        {
            _mediator = new Mock<IMediator>();
            var controller = new ClientsController(_mediator.Object);

            Task<ClientResponse> expected = Task.FromResult(new ClientResponse
            {
                Id = 1,
                Name = "Pedro",
                LastName = "Castillo"
            });

            _mediator
                .Setup(s => s.Send<ClientResponse>(new GetClientByIdQuery(1), It.IsAny<CancellationToken>()))
                .Returns(expected);

            ActionResult<ClientResponse> result = await controller.GetById(1);

            Assert.Equal(1, result?.Value?.Id);
        }
    }
}
