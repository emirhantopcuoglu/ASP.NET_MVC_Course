using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductsAPI.DTO;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public UsersController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser(UserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new AppUser
            {
                FullName = model.FullName,
                UserName = model.UserName,
                Email = model.Email,
                DateAdded = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return StatusCode(201);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return BadRequest(new { message = "email hatalı" });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (result.Succeeded)
            {
                return Ok(new { token = GenerateJWT(user) });
            }

            return Unauthorized(); // 403
        }

        private object GenerateJWT(AppUser user)
        {
            // 1. JWT tokeni oluşturmak için bir token işleyici nesnesi tanımlıyoruz.
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Token imzalamada kullanılacak gizli anahtarı (_configuration'dan alarak) tanımlıyoruz.
            // "_configuration.GetSection("AppSettings:Secret").Value" ile anahtarı alırız ve ASCII formatında kodlarız.
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value ?? "");

            // 3. Token descriptor (token tanımlayıcısı) oluşturuyoruz, bu descriptor token'in içeriğini belirleyecek.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // 4. Kullanıcı bilgilerini ("claims") ekliyoruz. Bu bilgiler token doğrulamasında kullanılacak.
                Subject = new ClaimsIdentity(
                    new Claim[]{
                // Kullanıcı ID'sini (NameIdentifier olarak) token'a claim olarak ekliyoruz.
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                
                // Kullanıcı adını (Name olarak) token'a claim olarak ekliyoruz.
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                    }
                ),

                // 5. Token'in geçerlilik süresini ayarlıyoruz. Burada, 1 gün sonra sona erecek şekilde ayarlandı.
                Expires = DateTime.UtcNow.AddDays(1),

                // 6. İmzalama kimlik bilgilerini tanımlıyoruz. Bu bilgiler token'in doğruluğunu sağlamak için kullanılır.
                // HMAC-SHA256 algoritması ile symetric bir anahtar (key) kullanılarak imzalanıyor.
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // 7. Token'i descriptor (tanımlayıcı) kullanarak oluşturuyoruz.
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 8. Token’i string formatında geri döndürüyoruz.
            return tokenHandler.WriteToken(token);
        }

    }
}