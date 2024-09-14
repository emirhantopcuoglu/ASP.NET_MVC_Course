using System.ComponentModel.DataAnnotations;

namespace MeetingApp.Models
{
    public class UserInfo
    {
        public int Id { get; set; } 
        [Required(ErrorMessage = "Ad alanı zorunlu")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Telefon numarası zorunlu")]
        public string? Phone { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Hatalı e-mail")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Katılım durumunuzu belirtiniz")]
        public bool? WillAttend { get; set; }


    }
}