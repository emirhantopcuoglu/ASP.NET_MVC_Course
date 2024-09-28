using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class Ogrenci
    {
        // id => primary key
        [Key]
        public int OgrenciId { get; set; }
        public string? OgrenciAd { get; set; }
        public string? OgrenciSoyd { get; set; }
        public string? Eposta { get; set; }
        public string? Telefon { get; set; }


    }
}