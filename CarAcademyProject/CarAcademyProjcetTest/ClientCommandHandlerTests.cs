using AutoMapper;
using CarAcademyProject.AutoMapping;
using CarAcademyProject.CommandHandlers.ClientHandlers;
using CarAcademyProjectDL.RepoInterfaces;
using CarAcademyProjectModels;
using CarAcademyProjectModels.MediatR.ClientCommands;
using CarAcademyProjectModels.Request;
using MediatR;
using Moq;

namespace CarAcademyProjcetTest
{
    public class ClientCommandHandlerTests
    {
        private IList<Client> _clients = new List<Client>()
        {
            new Client(){ Id = 1, Age = 20, CarId = 1, FirstName = "Ivan", LastName = "Ivanov"},
            new Client(){ Id = 2, Age = 23, CarId = 2, FirstName = "Ivan2", LastName = "Ivanov2"},
            new Client(){ Id = 3, Age = 24, CarId = 3, FirstName = "Ivan3", LastName = "Ivanov3" }
        };

        private readonly IMapper _mapperMock;
        private readonly Mock<IClientRepository> _clientRepositoryMock;
        private readonly Mock<IMediator> _mediatorMock;

        public ClientCommandHandlerTests()
        {
            var mockedMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappings());
            });

            _mapperMock = mockedMapper.CreateMapper();
            _clientRepositoryMock = new Mock<IClientRepository>();
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async void GetAllClients_CommandHandler()
        {
            //setup
            var expectetResult = 3;
            _clientRepositoryMock.Setup(x => x.GetAllClients()).ReturnsAsync(_clients);

            //inject
            var command = new GetAllClientsCommand();
            var handler = new GetAllClientCommandHandler(_clientRepositoryMock.Object);

            //act
            var currResult = await handler.Handle(command, It.IsAny<CancellationToken>());

            //assert
            
            Assert.NotNull(currResult);
            Assert.Equal(expectetResult, currResult.Count());
        }

        [Fact]
        public async void AddClient_CommandHandler_HappyPath()
        {
            //setup
            var clientRequest = new ClientRequest() { FirstName = "Ivan4", LastName = "Ivanov4", Age = 23, CarId = 4 };
            var client = new Client { Id = 4, FirstName = clientRequest.FirstName, LastName = clientRequest.LastName, Age = clientRequest.Age, CarId = clientRequest.CarId };

            var expectetResult = 4;
            _clientRepositoryMock.Setup(x => x.AddClient(It.IsAny<Client>())).Callback(() =>
            {
                _clients.Add(client);
            }).ReturnsAsync(client);

            //inject
            var command = new AddClientCommand(clientRequest);
            var handler = new AddClientCommandHandler(_clientRepositoryMock.Object, _mapperMock);

            //act
            var currResult = await handler.Handle(command, It.IsAny<CancellationToken>());

            //assert
            Assert.NotNull(currResult);
            Assert.Equal(expectetResult, _clients.Count());
        }

        [Fact]
        public async void AddClient_CommandHandler_BadRequest()
        {
            //setup

            //inject
            var handler = new AddClientCommandHandler(_clientRepositoryMock.Object, _mapperMock);

            //act
            var currResult = await handler.Handle(null, It.IsAny<CancellationToken>());

            //assert
            Assert.NotNull(currResult);
            Assert.Equal("BadRequest", currResult.HttpStatusCode.ToString());
            Assert.Equal("The request is null", currResult.Message);
        }

        [Fact]
        public async void UpdateClient_CommandHandler_HappyPath()
        {
            //setup
            var clientRequest = new ClientRequest() { FirstName = "Ivan2", LastName = "Ivanov2", Age = 25, CarId = 14 };
            var client = new Client { Id = 4, FirstName = clientRequest.FirstName, LastName = clientRequest.LastName, Age = clientRequest.Age, CarId = clientRequest.CarId };

            _clientRepositoryMock.Setup(x => x.GetClientByName(It.IsAny<string>()))
                .ReturnsAsync(_clients.FirstOrDefault(x => x.FirstName == clientRequest.FirstName && x.LastName == clientRequest.LastName));

            _clientRepositoryMock.Setup(x => x.UpdateClient(It.IsAny<Client>())).Callback(() =>
            {
                var currClient = _clients.FirstOrDefault(x=> x.FirstName == clientRequest.FirstName && x.LastName == clientRequest.LastName);
                if (currClient != null)
                {
                    currClient.Age = clientRequest.Age;
                    currClient.CarId = clientRequest.CarId;
                }
            }).ReturnsAsync(client);

            //inject
            var command = new UpdateClientCommand(clientRequest);
            var handler = new UpdateClientCommandHandler(_clientRepositoryMock.Object, _mapperMock);

            //act
            var currResult = await handler.Handle(command, It.IsAny<CancellationToken>());

            //assert
            Assert.NotNull(currResult);
            Assert.Equal("OK", currResult.HttpStatusCode.ToString());
            Assert.Equal(clientRequest.Age, currResult.Client.Age);
            Assert.Equal(clientRequest.CarId, currResult.Client.CarId);
        }

        [Fact]
        public async void UpdateClient_CommandHandler_ClientNotFound()
        {
            //setup
            var clientRequest = new ClientRequest() { FirstName = "Ivan6", LastName = "Ivanov6", Age = 25, CarId = 14 };
            var client = new Client { Id = 4, FirstName = clientRequest.FirstName, LastName = clientRequest.LastName, Age = clientRequest.Age, CarId = clientRequest.CarId };

            _clientRepositoryMock.Setup(x => x.GetClientByName(It.IsAny<string>()))
                .ReturnsAsync(_clients.FirstOrDefault(x => x.FirstName == clientRequest.FirstName && x.LastName == clientRequest.LastName));

            _clientRepositoryMock.Setup(x => x.UpdateClient(It.IsAny<Client>())).Callback(() =>
            {
                var currClient = _clients.FirstOrDefault(x => x.FirstName == clientRequest.FirstName && x.LastName == clientRequest.LastName);
                if (currClient != null)
                {
                    currClient.Age = clientRequest.Age;
                    currClient.CarId = clientRequest.CarId;
                }
            }).ReturnsAsync(client);

            //inject
            var command = new UpdateClientCommand(clientRequest);
            var handler = new UpdateClientCommandHandler(_clientRepositoryMock.Object, _mapperMock);

            //act
            var currResult = await handler.Handle(command, It.IsAny<CancellationToken>());

            //assert
            Assert.NotNull(currResult);
            Assert.Equal("BadRequest", currResult.HttpStatusCode.ToString());
            Assert.Equal("There is no such Client", currResult.Message);
        }

        [Fact]
        public async void DeleteClient_CommandHandler_HappyPath()
        {
            //setup
            var clientRequest = new ClientRequest() { FirstName = "Ivan2", LastName = "Ivanov2", Age = 25, CarId = 14 };
            var client = new Client { Id = 2, FirstName = clientRequest.FirstName, LastName = clientRequest.LastName, Age = clientRequest.Age, CarId = clientRequest.CarId };

            _clientRepositoryMock.Setup(x => x.GetClientByName(It.IsAny<string>()))
                .ReturnsAsync(_clients.FirstOrDefault(x => x.FirstName == clientRequest.FirstName && x.LastName == clientRequest.LastName));
            _clientRepositoryMock.Setup(x => x.DeleteClient(It.IsAny<int>())).Callback(() =>
            {
                var currClient = _clients.FirstOrDefault(x => x.Id == 2);
                _clients.Remove(currClient);
            }).ReturnsAsync(client);
            //inject
            var command = new DeleteClientCommand("Ivan2 Ivanov2");
            var handler = new DeleteClientCommandHandler(_clientRepositoryMock.Object);

            //act
            var currResult = await handler.Handle(command, It.IsAny<CancellationToken>());

            //assert
            Assert.NotNull(currResult);
            Assert.Equal("OK", currResult.HttpStatusCode.ToString());
            Assert.Equal(2, _clients.Count());
        }

        [Fact]
        public async void DeleteClient_CommandHandler_ClientNotFound()
        {
            //setup
            var clientRequest = new ClientRequest() { FirstName = "Ivan6", LastName = "Ivanov6", Age = 25, CarId = 14 };
            var client = new Client { Id = 4, FirstName = clientRequest.FirstName, LastName = clientRequest.LastName, Age = clientRequest.Age, CarId = clientRequest.CarId };

            _clientRepositoryMock.Setup(x => x.GetClientByName(It.IsAny<string>()))
                .ReturnsAsync(_clients.FirstOrDefault(x => x.FirstName == clientRequest.FirstName && x.LastName == clientRequest.LastName));

            //inject
            var command = new DeleteClientCommand("Ivan6 Ivanov6");
            var handler = new DeleteClientCommandHandler(_clientRepositoryMock.Object);

            //act
            var currResult = await handler.Handle(command, It.IsAny<CancellationToken>());

            //assert
            Assert.NotNull(currResult);
            Assert.Equal("BadRequest", currResult.HttpStatusCode.ToString());
            Assert.Equal("This client does not exists", currResult.Message);
        }
    }
}
