using FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra;
using FIAP.PosTech.ArqSistemas.CloudGames.Api.Infra.Middleware;
using FIAP.PosTech.ArqSistemas.CloudGames.Api.Interfaces;
using FIAP.PosTech.ArqSistemas.CloudGames.Api.Services;
using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Model;
using FIAP.PosTech.ArqSistemas.CloudGames.Domain.Validation;
using FluentValidation;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

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

#region DI
builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<ICorrelationIdGenerator, CorrelationIdGenerator>();
builder.Services.AddTransient(typeof(BaseLogger<>));
builder.Services.AddTransient<IValidator<Usuario>, UsuarioValidator>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCorrelationMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
