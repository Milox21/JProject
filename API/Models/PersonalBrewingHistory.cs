using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Models.ModelInterfaces;

namespace API.Models
{
    [Table("PersonalBrewingHistory")]
    public partial class PersonalBrewingHistory : IBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("personal_shelf_id")]
        public int PersonalShelfId { get; set; }
        [Column("temp")]
        public int Temp { get; set; }
        [Column("brewing_time")]
        public int BrewingTime { get; set; }
        [Column("amount", TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }
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

        [ForeignKey("PersonalShelfId")]
        [InverseProperty("PersonalBrewingHistories")]
        public virtual PersonalShelf PersonalShelf { get; set; } = null!;
    }
}
