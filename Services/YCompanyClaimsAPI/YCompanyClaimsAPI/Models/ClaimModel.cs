namespace YCompanyClaimsAPI.Models
{
    public class ClaimModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Value { get; set; }
        public int ClientId { get; set; }

    }
}
