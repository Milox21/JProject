using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Models.ModelInterfaces;

namespace API.Models
{
    [Table("TeaType")]
    public partial class TeaType : IBaseModel
    {
        public TeaType()
        {
            PromotionCodeTypes = new HashSet<PromotionCodeType>();
            Teas = new HashSet<Tea>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(255)]
        public string Name { get; set; } = null!;
        [Column("description")]
        public string Description { get; set; } = null!;
        [Column("temp")]
        public int Temp { get; set; }
        [Column("brewing_time")]
        public int BrewingTime { get; set; }
        [Column("amount_per_cup")]
        [StringLength(255)]
        public string AmountPerCup { get; set; } = null!;
        [Column("created_at", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }
        [Column("deleted_at", TypeName = "datetime")]
        public DateTime? DeletedAt { get; set; }
        [Column("is_active")]
        public bool? IsActive { get; set; }

        [InverseProperty("TeaType")]
        public virtual ICollection<PromotionCodeType>? PromotionCodeTypes { get; set; }
        [InverseProperty("TeaType")]
        public virtual ICollection<Tea>? Teas { get; set; }
    }
}
