using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductsAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductsContext>(x => x.UseSqlite("Data Source=products.db"));
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<ProductsContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;

    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
});
// Authentication (kimlik doğrulama) işlemlerini yapılandırmak için AddAuthentication metodu kullanılıyor.
builder.Services.AddAuthentication(x =>
{

    // Varsayılan kimlik doğrulama şeması olarak JWT kullanılıyor. Bu şema, kimlik doğrulama ve yetkilendirme işlemlerinde JWT'yi tercih edeceğini belirtiyor.
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

    // Varsayılan kimlik doğrulama zorluğu şeması olarak yine JWT kullanılıyor. Bu, yetkilendirilmemiş bir erişim olduğunda JWT şemasıyla işlem yapılacağını belirtir.
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(x =>
{

    // RequireHttpsMetadata, HTTP yerine HTTPS kullanma zorunluluğunu belirler. Geliştirme ortamında bu gereksinim kaldırılabilir.
    x.RequireHttpsMetadata = false;

    // Token doğrulama parametreleri burada ayarlanıyor.
    x.TokenValidationParameters = new TokenValidationParameters
    {
        // Token içindeki "Issuer" bilgisini doğrulayıp doğrulamayacağını belirtir. Burada "false" olarak ayarlandığı için doğrulama yapılmaz.
        ValidateIssuer = false,

        // Eğer ValidateIssuer true yapılmış olsaydı, geçerli bir "Issuer" belirlemek gerekirdi. Burada örnek olarak "deneme.com" verilmiş.
        ValidIssuer = "deneme.com",

        // Token içindeki "Audience" (hedef kitle) bilgisini doğrulayıp doğrulamayacağını belirtir. Burada "false" olarak ayarlandığı için doğrulama yapılmaz.
        ValidateAudience = false,

        // ValidAudience boş olarak ayarlanmış, bu yüzden belirli bir Audience yok.
        ValidAudience = "",

        // Bu parametre, birden fazla geçerli hedef kitlenin olup olmadığını belirlemek için kullanılır.
        ValidAudiences = new string[] { "a", "b" },

        // Token'ın imzasını doğrulamak için kullanılan anahtar bilgisini doğrular.
        ValidateIssuerSigningKey = true,

        // SymmetricSecurityKey, simetrik şifreleme anahtarını tanımlar. Bu anahtar, JWT'nin imzasını doğrulamak için kullanılır.
        // Secret anahtarı AppSettings bölümünde tutulur, burada bu değeri okuyarak anahtar oluşturuluyor.
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
            builder.Configuration.GetSection("AppSettings:Secret").Value ?? "")),

        // Token'ın geçerlilik süresini doğrular. Bu, token'ın belirlenen süre sonunda geçerliliğini yitireceği anlamına gelir.
        ValidateLifetime = true
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();