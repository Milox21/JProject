using API.Models.ModelInterfaces;

namespace MobileTeaApp.Models
{
    public class TeaTypeDTO : IBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Temp { get; set; }
        public int BrewingTime { get; set; }
        public string AmountPerCup { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool? IsActive { get; set; }

        public ICollection<TeaDTO>? Teas { get; set; }

        public PromotionCodeTypeDTO? PromotionCodeType { get; set; }
    }
}
