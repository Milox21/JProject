using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Models.ModelInterfaces;

namespace API.Models
{
    [Table("PromotionCodeType")]
    public partial class PromotionCodeType : IBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("tea_type_id")]
        public int TeaTypeId { get; set; }
        [Column("company_id")]
        public int CompanyId { get; set; }
        [Column("promo_code")]
        [StringLength(255)]
        public string PromoCode { get; set; } = null!;
        [Column("promo_amount")]
        public int PromoAmount { get; set; }
        [Column("promo_start", TypeName = "datetime")]
        public DateTime PromoStart { get; set; }
        [Column("promo_end", TypeName = "datetime")]
        public DateTime PromoEnd { get; set; }
        [Column("created_at", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }
        [Column("deleted_at", TypeName = "datetime")]
        public DateTime? DeletedAt { get; set; }
        [Column("is_active")]
        public bool? IsActive { get; set; }

        [ForeignKey("CompanyId")]
        [InverseProperty("PromotionCodeTypes")]
        public virtual Company Company { get; set; } = null!;
        [ForeignKey("TeaTypeId")]
        [InverseProperty("PromotionCodeTypes")]
        public virtual TeaType TeaType { get; set; } = null!;
    }
}
