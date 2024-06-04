using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Models.ModelInterfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    [Table("Profile")]
    [Index("Email", Name = "UQ__Profile__AB6E61643D7DA571", IsUnique = true)]
    public partial class Profile : IBaseModel
    {
        public Profile()
        {
            PersonalShelves = new HashSet<PersonalShelf>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("username")]
        [StringLength(255)]
        public string Username { get; set; } = null!;
        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; } = null!;
        [Column("password")]
        [StringLength(255)]
        public string Password { get; set; } = null!;
        [Column("created_at", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }
        [Column("deleted_at", TypeName = "datetime")]
        public DateTime? DeletedAt { get; set; }
        [Column("is_active")]
        public bool? IsActive { get; set; }

        [InverseProperty("Profile")]
        public virtual ICollection<PersonalShelf> PersonalShelves { get; set; }
    }
}
