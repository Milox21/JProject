using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Models.ModelInterfaces;

namespace API.Models
{
    [Table("Company")]
    public partial class Company : IBaseModel
    {
        public Company()
        {
            PromotionCodeTeas = new HashSet<PromotionCodeTea>();
            PromotionCodeTypes = new HashSet<PromotionCodeType>();
            Teas = new HashSet<Tea>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(255)]
        public string Name { get; set; } = null!;
        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; } = null!;
        [Column("phone_number")]
        [StringLength(50)]
        public string PhoneNumber { get; set; } = null!;
        [Column("created_at", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }
        [Column("deleted_at", TypeName = "datetime")]
        public DateTime? DeletedAt { get; set; }
        [Column("is_active")]
        public bool? IsActive { get; set; }

        [InverseProperty("Company")]
        public virtual ICollection<PromotionCodeTea> PromotionCodeTeas { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<PromotionCodeType> PromotionCodeTypes { get; set; }
        [InverseProperty("Company")]
        public virtual ICollection<Tea> Teas { get; set; }
    }
}
