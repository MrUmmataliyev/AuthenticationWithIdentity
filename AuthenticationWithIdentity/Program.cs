using AuthenticationWithIdentity;
using AuthenticationWithIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")); //SQL oilasidagi tillarni ishlatsa bo'ladi 
});

builder.Services.AddAuthentication();
builder.Services.AddIdentityApiEndpoints<AppUser>().AddEntityFrameworkStores<AppDbContext>(); // Microsoft identityni elonqilsh uchun //was IdentityUser
builder.Services.AddSwaggerGen(options => 
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization", // Boshqa nom qo'yilsa 401 qaytaradi
        Type=SecuritySchemeType.ApiKey,
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
}); //Swaggerda Authentication uchun 





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<AppUser>(); // Microsoft Identity controllerlarini API chiqarish uchun   //was IdentityUser
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
