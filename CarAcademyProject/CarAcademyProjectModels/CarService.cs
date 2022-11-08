using CarAcademyProjectModels.Enums;
using MessagePack;

namespace CarAcademyProjectModels
{
    public class CarService
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int CarId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ManipulationDifficult MDifficult { get; set; }

        public string ManipulationDescription { get; set; }
    }
}
