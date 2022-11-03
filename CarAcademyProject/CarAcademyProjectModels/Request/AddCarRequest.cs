using System.ComponentModel.DataAnnotations;

namespace CarAcademyProjectModels.Request
{
    public class AddCarRequest
    {
        public string PlateNumber { get; set; }

        public string Model { get; set; }

        public string Brand { get; set; }

        public int MaxSpeed { get; set; }

        public int ClientId { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
