using System.ComponentModel.DataAnnotations;

namespace WebApiDemo.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string Role { get; set; }
    }
}
