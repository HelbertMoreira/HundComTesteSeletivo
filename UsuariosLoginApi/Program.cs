using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UsuariosLoginApi.Data;
using UsuariosLoginApi.Models;
using UsuariosLoginApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApiLoginContext>(options =>
    options.UseLazyLoadingProxies()
    .UseSqlServer(builder.Configuration.GetConnectionString("ApiLoginContext") ??
    throw new InvalidOperationException("String de conexão cadastrada não encontrada.")));

builder.Services.AddIdentity<CustomIdentityUser, IdentityRole<int>>(opt =>
    { 
        opt.SignIn.RequireConfirmedEmail = true;
        
    })
    .AddEntityFrameworkStores<ApiLoginContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<EmailService, EmailService>();
builder.Services.AddScoped<CadastroService, CadastroService>();
builder.Services.AddScoped<TokenService, TokenService>();
builder.Services.AddScoped<LoginService, LoginService>();
builder.Services.AddScoped<LogoutService, LogoutService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
