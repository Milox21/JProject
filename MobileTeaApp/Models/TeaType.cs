﻿namespace MobileTeaApp.Models
{
    public class TeaType
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

        public ICollection<Tea>? Teas { get; set; }
        public PromotionCodeType? PromotionCodeType { get; set; }
    }
}
