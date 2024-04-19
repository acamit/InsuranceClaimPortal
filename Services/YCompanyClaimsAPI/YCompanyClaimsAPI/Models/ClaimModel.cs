namespace YCompanyClaimsAPI.Models
{
    public class ClaimModel
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Status { get; set; }

    }
}
