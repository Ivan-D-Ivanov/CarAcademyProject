using CarAcademyProjectModels.Enums;

namespace CarAcademyProjectModels.Request
{
    public class CarServiceRquest
    {
        public string ClientName { get; set; }

        public string CarPlateNumber { get; set; }

        public ManipulationDifficult MDifficult { get; set; }

        public string ManipulationDescription { get; set; }
    }
}
