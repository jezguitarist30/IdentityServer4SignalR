using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [MaxLength(150), Required]
        public string Name { get; set; }
        [MaxLength(150), EmailAddress, Required]
        public string Email { get; set; }
        [MaxLength(60), Required]
        public string Password { get; set; }
        [Required, DefaultValue(true)]
        public bool IsActive { get; set; }

        public ICollection<Claim> Claims { get; set; } = new List<Claim>();
    }
}
