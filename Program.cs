using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using sheridan_connect_backend.DB;
using sheridan_connect_backend.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.Configure<MongoDBSetting>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddSingleton<UserService>();

builder.Services.AddControllers();

// Configure JWT authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<AppSettings>();
if (string.IsNullOrEmpty(jwtSettings.JwtSecret))
{
    throw new Exception("JWT Secret is not configured!");
}
var key = Encoding.ASCII.GetBytes(jwtSettings.JwtSecret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});



var app = builder.Build();
var config =
    new ConfigurationBuilder()
        .AddEnvironmentVariables()
        .Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();