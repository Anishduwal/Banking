using System.ComponentModel.DataAnnotations;

namespace Banking.Models
{
    public class UserDetailsModel
    {
        public int Id { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "The confirm password not matched.")]
        public string? ConfirmPassword { get; set; }
        public int Code { get; set; }
        public string? Message { get; set; }
    }
}
