using AutoMapper;
using CarAcademyProject.AutoMapping;
using CarAcademyProject.Controllers;
using CarAcademyProjectDL.RepoInterfaces;
using CarAcademyProjectModels;
using CarAcademyProjectModels.MediatR.ClientCommands;
using CarAcademyProjectModels.Request;
using CarAcademyProjectModels.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CarAcademyProjcetTest
{
    public class ClientControllerTests
    {
        private IList<Client> _clients = new List<Client>()
        {
            new Client(){ Id = 1, Age = 20, CarId = 1, FirstName = "Ivan", LastName = "Ivanov"},
            new Client(){ Id = 2, Age = 23, CarId = 2, FirstName = "Ivan2", LastName = "Ivanov2"},
            new Client(){ Id = 3, Age = 24, CarId = 3, FirstName = "Ivan3", LastName = "Ivanov3" }
        };

        private readonly IMapper _mapperMock;
        private readonly Mock<ILogger<ClientController>> _loggerMock;
        private readonly Mock<IClientRepository> _clientRepositoryMock;
        private readonly Mock<IMediator> _mediatorMock;

        public ClientControllerTests()
        {
            var mockedMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappings());
            });

            _mapperMock = mockedMapper.CreateMapper();
            _loggerMock = new Mock<ILogger<ClientController>>();
            _clientRepositoryMock = new Mock<IClientRepository>();
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async void GetAllClients_Controller_HappyPath()
        {
            //setup
            var expectetResult = 3;
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetAllClientsCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(_clients);

            //inject
            var controller = new ClientController(_loggerMock.Object, _mediatorMock.Object);

            //act
            var currResult = await controller.GetAllClients();

            //assert
            var okObjectResult = currResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var clients = okObjectResult.Value as IEnumerable<Client>;
            Assert.NotEmpty(clients);
            Assert.NotNull(clients);
            Assert.Equal(expectetResult, clients.Count());
        }

        [Fact]
        public async void AddClient_Controller_HappyPath()
        {
            //setup
            var clientRequest = new ClientRequest() { FirstName = "Ivan4", LastName = "Ivanov4", Age = 23, CarId = 4 };

            var client = new Client { Id = 4, FirstName = clientRequest.FirstName, LastName = clientRequest.LastName, Age = clientRequest.Age, CarId = clientRequest.CarId };
            var clientResponse = new ClientResponse() { HttpStatusCode = System.Net.HttpStatusCode.OK };

            _mediatorMock.Setup(x => x.Send(It.IsAny<AddClientCommand>(), It.IsAny<CancellationToken>())).Callback(() =>
            {
                _clients.Add(client);
                clientResponse.Client = _clients.FirstOrDefault(x => x.Id == 4);
            }).ReturnsAsync(clientResponse);

            //inject
            var controller = new ClientController(_loggerMock.Object, _mediatorMock.Object);

            //act
            var currResult = await controller.AddClient(clientRequest);

            //assert
            var okObjectResult = currResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var clients = okObjectResult.Value as ClientResponse;
            Assert.NotNull(clients);
            Assert.Equal(4, clients.Client.Id);
        }

        [Fact]
        public async void AddClient_Controller_BadRequest()
        {
            //setup
            _mediatorMock.Setup(x => x.Send(It.IsAny<AddClientCommand>(), It.IsAny<CancellationToken>()))
                .Callback(() =>
                {

                }).ReturnsAsync(new ClientResponse { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "The request is null" });

            //inject
            var controller = new ClientController(_loggerMock.Object, _mediatorMock.Object);

            //act
            var currResult = await controller.AddClient(new ClientRequest());

            //assert
            var badRequestResult = currResult as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);

            var clients = badRequestResult.Value as ClientResponse;
            Assert.Null(clients.Client);
            Assert.Equal("The request is null", clients.Message);
        }

        [Fact]
        public async void UpdateClient_Controller_HappyPath()
        {
            //setup
            var clientRequest = new ClientRequest() { FirstName = "Ivan2", LastName = "Ivanov2", Age = 24, CarId = 2 };

            var clientResponse = new ClientResponse() { HttpStatusCode = System.Net.HttpStatusCode.OK };

            _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateClientCommand>(), It.IsAny<CancellationToken>())).Callback(() =>
            {
                var client = _clients.FirstOrDefault(x => x.FirstName == clientRequest.FirstName && x.LastName == clientRequest.LastName);
                if (client != null)
                {
                    client.Age = clientRequest.Age;
                    client.CarId = clientRequest.CarId;
                    clientResponse.Client = client;
                }
            }).ReturnsAsync(clientResponse);

            //inject
            var controller = new ClientController(_loggerMock.Object, _mediatorMock.Object);

            //act
            var currResult = await controller.UpdateClientByName(clientRequest);

            //assert
            var okObjectResult = currResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var clients = okObjectResult.Value as ClientResponse;
            Assert.NotNull(clients);
            Assert.Equal(24, clients.Client.Age);
            Assert.Equal(2, clients.Client.CarId);
        }

        [Fact]
        public async void UpdateClient_Controller_BadRequest_ClientNotFound()
        {
            //setup
            var clientRequest = new ClientRequest() { FirstName = "Ivan5", LastName = "Ivanov5", Age = 24, CarId = 2 };

            var clientResponse = new ClientResponse { HttpStatusCode = System.Net.HttpStatusCode.BadRequest };

            _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateClientCommand>(), It.IsAny<CancellationToken>())).Callback(() =>
            {
                var client = _clients.FirstOrDefault(x => x.FirstName == clientRequest.FirstName && x.LastName == clientRequest.LastName);
                if (client == null)
                {
                    clientResponse.Client = client;
                    clientResponse.Message = "There is no such Client";
                }
            }).ReturnsAsync(clientResponse);

            //inject
            var controller = new ClientController(_loggerMock.Object, _mediatorMock.Object);

            //act
            var currResult = await controller.UpdateClientByName(clientRequest);

            //assert
            var badRequestResult = currResult as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);

            var clients = badRequestResult.Value as ClientResponse;
            Assert.Null(clients.Client);
            Assert.Equal("There is no such Client", clients.Message);
        }

        [Fact]
        public async void DeleteClient_Controller_HappyPath()
        {
            //setup
            var clientRequest = new ClientRequest() { FirstName = "Ivan2", LastName = "Ivanov2", Age = 24, CarId = 2 };

            var clientResponse = new ClientResponse() { HttpStatusCode = System.Net.HttpStatusCode.OK };

            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteClientCommand>(), It.IsAny<CancellationToken>())).Callback(() =>
            {
                var client = _clients.FirstOrDefault(x => x.FirstName == clientRequest.FirstName && x.LastName == clientRequest.LastName);
                if (client != null)
                {
                    clientResponse.Client = client;
                    _clients.Remove(client);
                }
            }).ReturnsAsync(clientResponse);

            //inject
            var controller = new ClientController(_loggerMock.Object, _mediatorMock.Object);

            //act
            var currResult = await controller.DeleteClient("Ivan2 Ivanov2");

            //assert
            var okObjectResult = currResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var clients = okObjectResult.Value as ClientResponse;
            Assert.NotNull(clients);
            Assert.Equal(23, clients.Client.Age);
            Assert.Equal(2, clients.Client.CarId);
            Assert.Equal(2, _clients.Count());
        }

        [Fact]
        public async void DeleteClient_Controller_BadRequest_ClientNotFound()
        {
            //setup
            var clientRequest = new ClientRequest() { FirstName = "Ivan5", LastName = "Ivanov5", Age = 24, CarId = 2 };

            var clientResponse = new ClientResponse { HttpStatusCode = System.Net.HttpStatusCode.BadRequest };

            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteClientCommand>(), It.IsAny<CancellationToken>())).Callback(() =>
            {
                var client = _clients.FirstOrDefault(x => x.FirstName == clientRequest.FirstName && x.LastName == clientRequest.LastName);
                if (client == null)
                {
                    clientResponse.Client = client;
                    clientResponse.Message = "This client does not exists";
                }
            }).ReturnsAsync(clientResponse);

            //inject
            var controller = new ClientController(_loggerMock.Object, _mediatorMock.Object);

            //act
            var currResult = await controller.DeleteClient("Ivan5 Ivanov5");

            //assert
            var badRequestResult = currResult as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);

            var clients = badRequestResult.Value as ClientResponse;
            Assert.Null(clients.Client);
            Assert.Equal("This client does not exists", clients.Message);
        }
    }
}