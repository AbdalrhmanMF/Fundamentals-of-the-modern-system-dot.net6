using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.DAL.Models
{
    public class Source
    {
        public int Id { get; set; }

        public int SourceTypeId { get; set; }
        public virtual SourceType SourceType { get; set; }


        [Required,MaxLength(50)]
        public string Code { get; set; }

        [Required,MaxLength(250)]
        public string AName { get; set; }

        [Required,MaxLength(250)]
        public string EName { get; set; }

        [MaxLength(250)]
        public string? Address { get; set; }

        [MaxLength(50)]
        public string? Tel { get; set; }

        [MaxLength(50)]
        public string? Phone { get; set; }

        [MaxLength(50)]
        public string? Fax { get; set; }

        
        public int? GovernorateId { get; set; }
        public virtual Governorate Governorate { get; set; }
        
        public int? CityId { get; set; }        
        public virtual City City { get; set; }
    }
}
