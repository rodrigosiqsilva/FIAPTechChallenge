using FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra;
using FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra.Documentation;
using FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra.Log;
using FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra.Middleware;
using FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra.Repository;
using FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces;
using FIAP.PosTech.ArqSistemas.CloudGames.Api.Services;
using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;
using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{ 

    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { 
        Title = "FIAP Cloud Games (FCG)",
        Version = "v1",
        Description = "A FIAP Cloud Games (FCG) é uma plataforma de venda de jogos digitais e gestão de servidores para partidas on-line",
        Contact = new OpenApiContact
        {
            Name = "Rodrigo Siqueira Silva",
            Email = "rodrigosiqueirasilva@hotmail.com"
        },
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization - Por favor, insira 'Bearer' e o token JWT. Exemplo: 'Bearer 12345abcdef'"
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
              }
            },
            new List<string>()
          }
        });

    c.SchemaFilter<EnumSchemaFilter>();
});

#region
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Admin", policy => policy.RequireRole("Admin"));

#endregion

#region DI
builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<ICorrelationIdGenerator, CorrelationIdGenerator>();
builder.Services.AddTransient(typeof(BaseLogger<>));
builder.Services.AddTransient<IValidator<Usuario>, UsuarioValidator>();
#endregion


builder.Services.AddDbContext<ApplicationDbContext>(options => {

    options.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
}, ServiceLifetime.Scoped);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCorrelationMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
