using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportManagemnetSystem.Data
{
    public class VehicleDartaAddImage
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public int VehicleDartaId { get; set; }
        public string FilePath { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }


        [ForeignKey(nameof(VehicleDartaId))]
        public VehicleDarta VehicleDarta { get; set; }
    }
}
