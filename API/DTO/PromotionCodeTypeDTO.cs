using API.Models.ModelInterfaces;

namespace MobileTeaApp.Models
{
    public class PromotionCodeTypeDTO : IBaseModel
    {
        public int Id { get; set; }
        public int TeaTypeId { get; set; }
        public int CompanyId { get; set; }
        public string PromoCode { get; set; } = string.Empty;
        public int PromoAmount { get; set; }
        public DateTime PromoStart { get; set; }
        public DateTime PromoEnd { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool? IsActive { get; set; }
    }
}
