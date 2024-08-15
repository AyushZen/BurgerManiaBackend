using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API_BurgerManiaBackend.Options;
using API_BurgerManiaBackend.Services;

using API_BurgerManiaBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SecureWebApiSample.Services;
using Microsoft.AspNetCore.Mvc;
using API_BurgerManiaBackend.Models;
using Microsoft.AspNetCore.Authorization;

namespace API_BurgerManiaBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<BurgerManiaDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs")));

            builder.Services.AddControllers();
            //builder.Services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(
            //        policy =>
            //        {
            //            policy.WithOrigins("https://127.0.0.1:7271");
            //            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            //        });

            //});
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            
            
            // Add services to the container.
            builder.Services.AddEndpointsApiExplorer();

            JwtSettings jwtSettings = new JwtSettings();
            builder.Configuration.Bind("JwtSettings",jwtSettings);
            builder.Services.AddSingleton(jwtSettings);
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddAuthentication((options) =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer((options) =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                };
            });

            builder.Services.AddSwaggerGen();
            //builder.Services.AddSwaggerGen((options) =>
            //{
            //    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
            //    {
            //        In = ParameterLocation.Header,
            //        Description = "Enter a valid Token",
            //        Name = "Authorization",
            //        Type = SecuritySchemeType.Http,
            //        BearerFormat = "JWT",
            //        Scheme = "Bearer"
            //    });
            //    options.AddSecurityRequirement(new OpenApiSecurityRequirement() {
            //        {
            //            new OpenApiSecurityScheme(){
            //                Reference=new OpenApiReference()
            //                {
            //                    Type=ReferenceType.SecurityScheme,
            //                    Id="Bearer"
            //                }
            //            },
            //            new string[]
            //            {

            //            }
            //        }
            //    });
            //});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //app.UseCors();
            app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true));
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}







//// GET: api/GetUserByMobileNo/{mobileNo} to get user by mobile number
//[HttpGet("GetUserByMobileNo/{mobileNo}")]
//public IActionResult GetUserByMobileNo(string mobileNo)
//{
//    Console.WriteLine($"mobileNo: {mobileNo}");
//    // Assuming you have a DbSet<User> in your DbContext
//    var user = _context.UserDatas.FirstOrDefault(u => u.Number == mobileNo);

//    if (user == null)
//    {
//        return NotFound($"User with mobile number {mobileNo} not found.");
//    }
//    var token = _tokenService.GenerateToken(mobileNo);
//    return Ok(new { user, Token = token }); // Return the user as JSON
//}






//// GET : "user/{userId}"  - Get used orders using order id
//[HttpGet("user/{userId}")]
//[Authorize]
//public async Task<ActionResult<IEnumerable<OrdersData>>> GetOrdersDatas(Guid userId)
//{
//    var orderData = await _context.OrdersDatas
//        .Where(o => o.UserId == userId)
//        .ToListAsync();

//    if (orderData == null || !orderData.Any())
//    {
//        return NotFound();
//    }

//    return orderData;
//}