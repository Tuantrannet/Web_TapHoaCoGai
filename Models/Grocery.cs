using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Grocery
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin bắt buộc.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin bắt buộc.")]
        public string Type{ get; set; }
        public double LowPrice { get; set; }
        public double HighPrice { get; set; }
        public string State { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime ExpiryDate { get; set; }


    }
}
