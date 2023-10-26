using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PruebaTecnicaMultitenant.Src.Application.Services;
using PruebaTecnicaMultitenant.Src.Application.UseCases;
using PruebaTecnicaMultitenant.Src.Domain.Services;
using PruebaTecnicaMultitenant.Src.Infrastructure.Middlewares;
using PruebaTecnicaMultitenant.Src.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IAuthTokenGenerator, AuthTokenGenerator>();
builder.Services.AddTransient<IDbMigrationsService, DbMigrationsService>();
builder.Services.AddTransient<IOrganizationsService, OrganizationsService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<OrganizationsUseCases>();
builder.Services.AddTransient<ProductsUseCases>();
builder.Services.AddTransient<UsersUseCases>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt => 
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:JwtKey"])
            ),
            ValidateLifetime = true, 
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseDbInitialization();

app.UseAuthorization();

app.UseTenantId();

app.MapControllers();

app.Run();
