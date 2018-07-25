using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Claim
    {
        public Claim()
        {

        }

        public Claim(string claimType, string claimValue)
        {
            ClaimType = claimType;
            ClaimValue = claimValue;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [MaxLength(250), Required]
        public string ClaimType { get; set; }
        [MaxLength(250), Required]
        public string ClaimValue { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; }
    }
}
