using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Models.ModelInterfaces;

namespace API.Models
{
    [Table("PersonalShelf")]
    public partial class PersonalShelf : IBaseModel
    {
        public PersonalShelf()
        {
            PersonalBrewingHistories = new HashSet<PersonalBrewingHistory>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("profile_id")]
        public int ProfileId { get; set; }
        [Column("tea_id")]
        public int TeaId { get; set; }
        [Column("amount", TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }
        [Column("rating")]
        public int? Rating { get; set; }
        [Column("note")]
        public string? Note { get; set; }
        [Column("created_at", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }
        [Column("deleted_at", TypeName = "datetime")]
        public DateTime? DeletedAt { get; set; }
        [Column("is_active")]
        public bool? IsActive { get; set; }

        [ForeignKey("ProfileId")]
        [InverseProperty("PersonalShelves")]
        public virtual Profile Profile { get; set; } = null!;
        [ForeignKey("TeaId")]
        [InverseProperty("PersonalShelves")]
        public virtual Tea Tea { get; set; } = null!;
        [InverseProperty("PersonalShelf")]
        public virtual ICollection<PersonalBrewingHistory> PersonalBrewingHistories { get; set; }
    }
}
