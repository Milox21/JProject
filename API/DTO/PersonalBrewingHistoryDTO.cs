using API.Models;
using API.Models.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTeaApp.Models
{
    public class PersonalBrewingHistoryDTO : IBaseModel
    {
        public int Id { get; set; }
        public int PersonalShelfId { get; set; }
        public int Temp { get; set; }
        public int BrewingTime { get; set; }
        public decimal Amount { get; set; }
        public string? Note { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool? IsActive { get; set; }
        public TeaDTO? Tea { get; set; }
    }
}
