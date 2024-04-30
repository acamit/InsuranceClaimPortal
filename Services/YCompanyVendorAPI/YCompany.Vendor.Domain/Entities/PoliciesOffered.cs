using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCompany.Vendor.Domain.Entities
{
    public class PoliciesOffered
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Vendors")]
        public int VendorId { get; set; }

        public int PolicyId { get; set; }
    }
}
