using CarAcademyProjectBL.BusinesService;
using CarAcademyProjectDL.RepoInterfaces;
using CarAcademyProjectModels;
using CarAcademyProjectModels.Enums;
using Microsoft.Extensions.Logging;
using Moq;

namespace CarAcademyProjcetTest
{
    public class BusinesServiceTests
    {
        private IList<Client> _clients = new List<Client>()
        {
            new Client(){ Id = 1, Age = 20, CarId = 1, FirstName = "Ivan", LastName = "Ivanov"},
            new Client(){ Id = 2, Age = 23, CarId = 2, FirstName = "Ivan2", LastName = "Ivanov2"},
            new Client(){ Id = 3, Age = 24, CarId = 3, FirstName = "Ivan3", LastName = "Ivanov3" }
        };

        IList<CarService> _carServices = new List<CarService>()
        {
            new CarService{Id = 1, CarId = 1, ClientId = 1,
                StartDate = DateTime.Parse("2022/1/10"), EndDate = DateTime.Parse("2022/5/10"), MDifficult = (ManipulationDifficult)1, ManipulationDescription = "1"},
            new CarService{Id = 2, CarId = 2, ClientId = 1,
                StartDate = DateTime.Parse("2022/2/10"), EndDate = DateTime.Parse("2022/6/10"), MDifficult = (ManipulationDifficult)1, ManipulationDescription = "1"},
            new CarService{Id = 3, CarId = 3, ClientId = 3,
                StartDate = DateTime.Parse("2022/3/10"), EndDate = DateTime.Parse("2022/7/10"), MDifficult = (ManipulationDifficult)1, ManipulationDescription = "1"}
        };

        private readonly Mock<ICarServiceRepository> _carServiceRepoMock;
        private readonly Mock<ILogger<ReportService>> _loggerMock;
        private readonly Mock<IClientRepository> _clientRepoMock;

        public BusinesServiceTests()
        {
            _carServiceRepoMock = new Mock<ICarServiceRepository>();
            _clientRepoMock = new Mock<IClientRepository>();
            _loggerMock = new Mock<ILogger<ReportService>>();
        }

        [Fact]
        public async Task ReportService_GetAllCarServices_FromDate()
        {
            //setup
            var start = "2022/1/11";
            var end = "2022/8/11";
            var expectedResult = 2;

            _carServiceRepoMock.Setup(x => x.GetAllCarServices()).ReturnsAsync(_carServices);

            //inject
            var service = new ReportService(_carServiceRepoMock.Object, _loggerMock.Object, _clientRepoMock.Object);

            //act
            var currResult = await service.GetCarServicesForTime(start, end);

            //assert
            Assert.Equal(expectedResult, currResult.Count());
        }

        [Fact]
        public async Task ReportService_GetAllCarServices_FromCLientName()
        {
            //setup
            var name = "Ivan Ivanov";
            var expectedResult = 2;

            _clientRepoMock.Setup(x => x.GetClientByName(It.IsAny<string>())).ReturnsAsync(_clients.FirstOrDefault(x => x.FirstName + " " + x.LastName == name));

            _carServiceRepoMock.Setup(x => x.GetAllCarServices()).ReturnsAsync(_carServices);

            //inject
            var service = new ReportService(_carServiceRepoMock.Object, _loggerMock.Object, _clientRepoMock.Object);

            //act
            var currResult = await service.GetClientServices(name);

            //assert
            Assert.Equal(expectedResult, currResult.Count());
        }
    }
}
