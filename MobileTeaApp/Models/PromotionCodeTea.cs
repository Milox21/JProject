﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTeaApp.Models
{
    public class PromotionCodeTea
    {
        public int Id { get; set; }
        public int TeaId { get; set; }
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