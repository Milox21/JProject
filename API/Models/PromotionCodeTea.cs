using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Models.ModelInterfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    [Table("PromotionCodeTea")]
    public partial class PromotionCodeTea : IBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("tea_id")]
        public int TeaId { get; set; }
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
        [InverseProperty("PromotionCodeTeas")]
        public virtual Company Company { get; set; } = null!;
        [ForeignKey("TeaId")]
        [InverseProperty("PromotionCodeTeas")]
        public virtual Tea Tea { get; set; } = null!;
    }
}
