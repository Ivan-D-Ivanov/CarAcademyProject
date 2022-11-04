using CarAcademyProjectModels.Enums;
using MessagePack;

namespace CarAcademyProjectModels.Request
{
    [MessagePackObject]
    public class PublishCarServiceRequest
    {
        [Key(1)]
        public string ClientName { get; set; }

        [Key(2)]
        public string CarPlateNumber { get; set; }

        [Key(3)]
        public ManipulationDifficult MDifficult { get; set; }

        [Key(4)]
        public string ManipulationDescription { get; set; }
    }
}
