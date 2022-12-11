using System.ComponentModel.DataAnnotations;


namespace OnlineShop.Models
{
    public class SpecialTags
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Special Tag") ]
        public string SpecialTag { get; set; }
    }
}
