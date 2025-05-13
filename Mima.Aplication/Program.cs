using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mima.Application.Cache;
using Mima.Application.Mappers;
using Mima.Application.Services.Abstraction;
using Mima.Application.Services.Implementation;
using Mima.Application.Utils;
using Mima.Domain.Model;
using Mima.Infrastructure;
using Mima.Infrastructure.Repositories.Abstraction;
using Mima.Infrastructure.Repositories.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MimaContext>(op =>
    op.UseSqlite(builder.Configuration.GetConnectionString("MimaConect"))
);

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<MimaContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        ValidateAudience = true,
        ValidateIssuer= true,
        ValidateIssuerSigningKey = true,


    };
});


builder.Services.AddControllers();
builder.Services.AddMemoryCache();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddAutoMapper(typeof(AuthMapper),typeof(ProductMapper),typeof(CategoryMapper),typeof(SaleMapper));
//builder.Services.AddAutoMapper(typeof(ProductMapper));

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<GetUserAuth>();
builder.Services.AddScoped<ICacheService, MemoryCacheService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("newPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
builder.Services.AddAuthorization();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("newPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
