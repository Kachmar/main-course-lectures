namespace ASP.NET.Demo.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreateRoleViewModel
    {
        [Required]
        public string Role { get; set; }
    }
}
