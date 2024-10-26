using System.ComponentModel.DataAnnotations;

namespace Route.Talabat.Dashboard.Models
{
    public class RoleFormViewModel
    {
        [Required(ErrorMessage = "Name is Required !")]
        [StringLength(256)]
        public string Name { get; set; }
    }
}
