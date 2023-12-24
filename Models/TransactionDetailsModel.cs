using System.ComponentModel.DataAnnotations;

namespace Banking.Models
{
    public class TransactionDetailsModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string? AccountNumber { get; set; }
        [Required]
        public string? TransactionType { get; set; }
        [Required]
        public decimal TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Remarks { get; set; }
        public decimal amount { get; set; }
    }
}
