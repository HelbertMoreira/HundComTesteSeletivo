using HundCom_Postagem.Services;
using HundCom_Postagem.Services.ImplementationServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext") ??
    throw new InvalidOperationException("String de conexão não encontrada.")));

// Adicionando a autenticação via token
builder.Services.AddAuthentication(auth =>
{
    // Indicando que a autenticação terá um Jwt
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    // Provando que o Jwt é verdadeiro
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Algumas definições da nossa autenticação
.AddJwtBearer(token =>
{
    token.RequireHttpsMetadata = false;
    // Armazenando o token
    token.SaveToken = true;

    // Os parâmetros que seram validado pelo sistema
    token.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        /*
         * O valor deve ser o mesmo que foi definido no
         * TokenService do UsuariosApi (onde o token foi
         * gerado)
         * 
         */
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("0iatamd090ksdiyg090bhgf090kjloit090wadfrs")),
        ValidateIssuer = false,
        ValidateAudience = false,
        /*
         * Como o nosso Token tem validade de uma hora,
         * ele ao autenticar o token, vai contar apartir do
         * zero o tempo de uma hora.
         * 
         */
        ClockSkew = TimeSpan.Zero
    };
});

// Add o AutoMapper
// Para isso devemos passar alguns parâmetros.
// Esses parâmetros é para que ele seja usado direto no assembli.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add o Services
// Injetando a dependência.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<AuthenticatedUser>();
builder.Services.AddScoped<ITopicoServices, TopicoServicesImplementation>();
builder.Services.AddScoped<IPostagemServices, PostagemServicesImplementation>();
builder.Services.AddScoped<IComentarioServices, ComentarioServicesImplementation>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Autenticando o meu token
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
