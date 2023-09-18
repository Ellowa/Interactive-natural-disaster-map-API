using InteractiveNaturalDisasterMap.Application;
using InteractiveNaturalDisasterMap.DataAccess.PostgreSql;
using InteractiveNaturalDisasterMap.Infrastructure.ServicesRegistration;
using InteractiveNaturalDisasterMap.Web.Middlewares.ExceptionHandling;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JSON Web Access Token" +
                      "<br>!!!ATTENTION!!!" +
                      "<br>The Token should be passed in header the next way:\n" +
                      "<br>\"Authorization: bearer <i>token</i>\"" +
                      "<br><br>Put the word <b>bearer</b> and <b>at least one space</b> before token in Value input!<br>",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }
        }
    });
});

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureDataAccessPostgreSqlServices(builder.Configuration);
builder.Services.ConfigureInfrastructureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors(policyBuilder =>
    policyBuilder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
