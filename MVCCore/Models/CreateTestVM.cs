using System.ComponentModel.DataAnnotations;

namespace MVCCore.Models
{
    public class CreateTestVM
    {
        [Display(Name="name"),Required(ErrorMessage ="requiredName")]
        public string Name { get; set; }
    }
}
