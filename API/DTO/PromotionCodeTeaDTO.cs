﻿using API.Models;
using API.Models.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTeaApp.Models
{
    public class PromotionCodeTeaDTO : IBaseModel
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