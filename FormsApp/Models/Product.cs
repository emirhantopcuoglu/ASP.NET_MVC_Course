using System.ComponentModel.DataAnnotations;

namespace FormsApp.Models
{
    public class Product
    {
        [Display(Name = "Urun Id")]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Urun Adı")]
        public string Name { get; set; } = null!;

        [Required]
        [Range(0,1000000)]
        [Display(Name = "Fiyat")]
        public decimal? Price { get; set; }

        [Display(Name = "Gorsel")]
        public string Image { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        [Display(Name = "Category")]
        [Required]
        public int? CategoryId { get; set; } 
    }
}