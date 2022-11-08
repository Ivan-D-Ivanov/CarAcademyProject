using CarAcademyProjectModels.Enums;

namespace CarAcademyProjectModels.Response
{
    
    public class MongoCarService
    {
        public string Id { get; set; }

        public string ClientName { get; set; }

        public string CarPlateNumber { get; set; }

        public ManipulationDifficult MDifficult { get; set; }

        public string ManipulationDescription { get; set; }
    }
}
