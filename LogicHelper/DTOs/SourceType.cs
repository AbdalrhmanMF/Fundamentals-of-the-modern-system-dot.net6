using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.DAL.Models
{
    public class SourceTypeDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Code { get; set; }

        [Required, MaxLength(200)]
        public string AName { get; set; }

        [Required, MaxLength(200)]
        public string EName { get; set; }

        [MaxLength(200)]
        public string? ADisplayName { get; set; }

        [MaxLength(200)]
        public string? EDisplayName { get; set; }

        public int? SrcType { get; set; }
    }
}
