using System.Security.Claims;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Posts");
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName || x.Email == model.Email);
                if (user == null)
                {
                    _userRepository.CreateUser(new User
                    {
                        UserName = model.UserName,
                        Name = model.Name,
                        Email = model.Email,
                        Password = model.Password,
                        Image = "p3.jpg"
                    });
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Username ya da email kullanımda.");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model) // Bu metot, kullanıcının giriş bilgilerini doğrular, eğer doğruysa kullanıcıyı oturum açmış olarak işaretler ve ilgili sayfaya yönlendirir.
        {
            if (ModelState.IsValid)
            {
                var isUser = _userRepository.Users.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);
                if (isUser != null)
                {
                    // Kullanıcının kimlik bilgilerini (claims) oluşturmak için bir liste tanımlanır.
                    // Bu bilgiler kullanıcının kimliği ve yetkilendirilmesi için gereklidir.
                    var userClaims = new List<Claim>();
                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()));// Kullanıcının benzersiz kimlik numarası (UserId) kimlik bilgileri arasına eklenir.
                    userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? ""));// Kullanıcının kullanıcı adı kimlik bilgileri arasına eklenir (boş olma durumu kontrol edilir).
                    userClaims.Add(new Claim(ClaimTypes.GivenName, isUser.Name ?? ""));// Kullanıcının adı kimlik bilgilerine eklenir (boş olma durumu kontrol edilir).
                    userClaims.Add(new Claim(ClaimTypes.UserData, isUser.Image ?? ""));

                    //Eğer kullanıcının e-posta adresi "info@admin.com" ise, bu kullanıcıya admin yetkisi (rolü) atanır.
                    if (isUser.Email == "info@admin.com")
                    {
                        userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                    }

                    // Kullanıcı kimlik bilgileriyle (claims) yeni bir ClaimsIdentity nesnesi oluşturulur.
                    // Bu kimlik, kullanıcının giriş yaptığına dair bilgileri tutar.
                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // AuthenticationProperties nesnesi oluşturulur ve kullanıcı oturumunun kalıcı olup olmayacağı ayarlanır.
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true // IsPersistent = true demek, oturumun tarayıcı kapansa bile açık kalacağı anlamına gelir (kalıcı oturum).
                    };

                    // Eğer kullanıcı zaten oturum açmışsa önce mevcut oturum kapatılır.
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    // Kullanıcı oturumu açılır ve oluşturulan kimlik bilgileri (ClaimsIdentity) ve ayarlar (AuthenticationProperties) kullanılarak giriş yapılır.
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,  // Kullanılan kimlik doğrulama şeması (Cookie tabanlı)
                        new ClaimsPrincipal(claimsIdentity), // Kullanıcının kimlik bilgileri
                        authProperties); // Oturum ayarları (kalıcılık bilgisi)

                    return RedirectToAction("Index", "Posts");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                }
            }

            return View(model);
        }
    }
}