using API.Models.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTeaApp.Models
{
    public class TeaDTO : IBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TeaTypeId { get; set; }
        public int CompanyId { get; set; }
        public string CountryOrigin { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Size { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool? IsActive { get; set; }

        public CompanyDTO? Company { get; set; }
        public TeaTypeDTO? TeaType { get; set; }
        public PromotionCodeTeaDTO? PromotionCodeTea { get; set; }
    }
}
