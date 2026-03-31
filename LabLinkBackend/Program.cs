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

var builder = WebApplication.CreateBuilder(args);//creating the builder object
 
builder.Services.AddControllers();//to handle requests look for controllers
 
builder.Services.AddFluentValidationAutoValidation();//It verifies the dataa comming from the user follows all the rules and constraints
 
builder.Services.AddValidatorsFromAssemblyContaining<LoginDTOValidator>();
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured");
var jwtIssuer = builder.Configuration["Jwt:Issuer"];//fetching from program.cs
 
builder.Services.AddAuthentication(options => //to use the bearer token. i want to secure my app and heres how to do that
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;//look for the jwt beare token in the url
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;//if no token send 401
})
.AddJwtBearer(options =>
{//strict rules for accepting the token
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
 
builder.Services.AddEndpointsApiExplorer();//to use swagger it discovers the routes
builder.Services.AddSwaggerGen();
 

builder.Services.AddDbContext<LabLinkDbContext>( // to connect wiith the database registering the context
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<LabLinkBackend.Repositories.IAuditLogRepository, LabLinkBackend.Repositories.AuditLogRepository>();
builder.Services.AddScoped<LabLinkBackend.Services.IAuditLogService, LabLinkBackend.Services.AuditLogService>();
 
var app = builder.Build();//all the configuration is done and web app instance to be created
 
// 3. Configure the HTTP request pipeline
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }
//pipeline of middlewares that every request must go through
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
 
app.MapControllers();
 
app.Run();
