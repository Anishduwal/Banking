using System.ComponentModel.DataAnnotations;

namespace Banking.Models
{
    public class AccountDetailsModel
    {
        public int Id { get; set; }
        [Required]
        public string? AccountHolder { get; set; }
        public string? AccountNumber { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
