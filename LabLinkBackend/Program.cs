using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using LabLinkBackend.Models;
using FluentValidation.AspNetCore;
using FluentValidation;
using LabLinkBackend.Validation;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddControllers();
 
builder.Services.AddFluentValidationAutoValidation();
 
builder.Services.AddValidatorsFromAssemblyContaining<LoginDTOValidator>();
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured");
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
 
builder.Services.AddAuthentication(options => 
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 

builder.Services.AddDbContext<LabLinkDbContext>( 
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<LabLinkBackend.Repositories.IAuditLogRepository, LabLinkBackend.Repositories.AuditLogRepository>();
builder.Services.AddScoped<LabLinkBackend.Services.IAuditLogService, LabLinkBackend.Services.AuditLogService>();
 
var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
 
app.MapControllers();
 
app.Run();
