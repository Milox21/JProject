using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Models.ModelInterfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    [Table("Tea")]
    public partial class Tea : IBaseModel
    {
        public Tea()
        {
            PersonalShelves = new HashSet<PersonalShelf>();
            PromotionCodeTeas = new HashSet<PromotionCodeTea>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(255)]
        public string Name { get; set; } = null!;
        [Column("tea_type_id")]
        public int TeaTypeId { get; set; }
        [Column("company_id")]
        public int CompanyId { get; set; }
        [Column("country_origin")]
        [StringLength(255)]
        public string CountryOrigin { get; set; } = null!;
        [Column("price", TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        [Column("size")]
        public int Size { get; set; }
        [Column("created_at", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }
        [Column("deleted_at", TypeName = "datetime")]
        public DateTime? DeletedAt { get; set; }
        [Column("is_active")]
        public bool? IsActive { get; set; }

        [ForeignKey("CompanyId")]
        [InverseProperty("Teas")]
        public virtual Company Company { get; set; } = null!;
        [ForeignKey("TeaTypeId")]
        [InverseProperty("Teas")]
        public virtual TeaType TeaType { get; set; } = null!;
        [InverseProperty("Tea")]
        public virtual ICollection<PersonalShelf> PersonalShelves { get; set; }
        [InverseProperty("Tea")]
        public virtual ICollection<PromotionCodeTea> PromotionCodeTeas { get; set; }
    }
}
