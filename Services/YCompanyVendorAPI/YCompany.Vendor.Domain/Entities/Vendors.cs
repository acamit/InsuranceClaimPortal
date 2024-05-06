using System.ComponentModel.DataAnnotations;

namespace YCompany.Vendor.Domain.Entities
{
    public class Vendors
    {
        [Key]
        public int VendorId { get; set; }

        public string? VendorName { get; set; }

        public string? ContactNumber { get; set; }

        public string? Email { get; set; }

        public PoliciesOffered? PoliciesOffered { get; set; }
    }
}
