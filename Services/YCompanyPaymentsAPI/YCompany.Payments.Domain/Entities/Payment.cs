using System.ComponentModel.DataAnnotations;

namespace YCompany.Payments.Domain.Enitites
{
    public class Payment
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Policy name is required")]
        public string PolicyName { get; set; }
    }
}
