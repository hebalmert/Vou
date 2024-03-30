using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using Vou.API.APIServices;
using Vou.API.Data;
using Vou.API.Helper;
using Vou.Shared.Entities;
using Vou.Shared.Enum;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Evita la redundancia Ciclica
builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sales API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. <br /> <br />
                      Enter 'Bearer' [space] and then your token in the text input below.<br /> <br />
                      Example: 'Bearer 12345abcdef'<br /> <br />",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,
            },
            new List<string>()
          }
        });
});

//Para La conexion a la base de datos
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=DefaultConnection"));

//Para Implementar el Identity y Validacion de Usuarios
builder.Services.AddIdentity<User, IdentityRole>(x =>
{
    //Cuando vamos colocar validacion de cuenta por correo
    x.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
    x.SignIn.RequireConfirmedEmail = true;

    x.User.RequireUniqueEmail = true;
    x.Password.RequireDigit = false;
    x.Password.RequiredUniqueChars = 0;
    x.Password.RequireLowercase = false;
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireUppercase = false;
    //Vamos a bloquear al cliente por 5 minutos
    //despues de 3 minutos
    x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    x.Lockout.MaxFailedAccessAttempts = 3;
    x.Lockout.AllowedForNewUsers = true;

})
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

//Para crear el JWTBearer y poderlo usar en la aplicacion
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x => x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwtKey"]!)),
        ClockSkew = TimeSpan.Zero
    });

//Agregamos Tipo de Claim para validar tipo de usuarios por Web API
//los mismo que usamos en los Roles MVC se declaran en el Claims Web API Role
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy(UserType.Admin.ToString(), x => x.RequireClaim(UserType.Admin.ToString()));
    option.AddPolicy(UserType.User.ToString(), x => x.RequireClaim(UserType.User.ToString()));
    option.AddPolicy(UserType.Technical.ToString(), x => x.RequireClaim(UserType.Technical.ToString()));
    option.AddPolicy(UserType.UserAux.ToString(), x => x.RequireClaim(UserType.UserAux.ToString()));
    option.AddPolicy(UserType.Cachier.ToString(), x => x.RequireClaim(UserType.Cachier.ToString()));
});

builder.Services.AddTransient<SeedDb>();
builder.Services.AddScoped<IAPIService, APIService>();
builder.Services.AddScoped<IComboHelper, ComboHelper>();
builder.Services.AddScoped<IUserHelper, UserHelper>();


//Inicio de Area de los Serviciios
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("https://localhost:7219") // dominio de tu aplicación Blazor
               .AllowAnyHeader()
               .AllowAnyMethod()
               .WithExposedHeaders(new string[] { "Totalpages", "conteo" });
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("https://localhost:7219") // dominio de tu aplicación Blazor
               .AllowAnyHeader()
               .AllowAnyMethod()
               .WithExposedHeaders(new string[] { "Totalpages", "conteo" });
    });
});


var app = builder.Build();
//Implementacion del SeedDb para llenado de Base de datos y Otros
SeedDb(app);
void SeedDb(WebApplication app)
{
    IServiceScopeFactory? scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (IServiceScope? scope = scopedFactory!.CreateScope())
    {
        SeedDb? service = scope.ServiceProvider.GetService<SeedDb>();
        service!.SeedAsync().Wait();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Llamar el Servicio de CORS
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

//Para Manejo de Imagenes En Disco
app.UseStaticFiles();

//Manejo de Usuario Logueados
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
