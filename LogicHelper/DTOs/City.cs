using System.ComponentModel.DataAnnotations;

namespace POS.DAL.Models
{
    public class CityDTO
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Code { get; set; }

        [Required, MaxLength(200)]
        public string AName { get; set; }

        [Required, MaxLength(200)]
        public string EName { get; set; }

        [Required]

        public int GovernorateId { get; set; }
        public List<GovernorateDTO> Governorates { get; set; }
    }
}
