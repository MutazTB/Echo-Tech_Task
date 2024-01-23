using AutoMapper;
using Domain.DTOs;
using Domain.Entiteis;
using Domain.Security;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Repositories.IRepositories;
using Repositories.Repositories;
using Services.IServices;
using Services.Services;
using Microsoft.AspNetCore.Identity;
using Repositories.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EchoTaskDBContext>(options => {
    // Our DATABASE_URL from js days
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "EchoTask_API",
        Version = "v1",
        Description = "API",
        Contact = new OpenApiContact
        {
            Email = "info@Echo-Tech.com",
            Name = "Echo_tech",
            Url = new Uri("http://Echo-Tech.com/")
        },
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer ",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer asdasdq313131afwe341234dfdg\""
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                },
                Name="Bearer"

            },new List<string>()
        }
    });
});


builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT_Secret:Issuer"],
        ValidAudience = builder.Configuration["JWT_Secret:Audience"],
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT_Secret:Key"])),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<EchoTaskDBContext>()
        .AddDefaultTokenProviders();


builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserSvc, UserSvc>();
builder.Services.AddScoped<IComplaintRepo, ComplaintRepo>();
builder.Services.AddScoped<IComplaintSvc, ComplaintSvc>();
builder.Services.AddScoped<IDemandRepo, DemandRepo>();
builder.Services.AddScoped<IDemandSvc, DemandSvc>();


JWT_Secret.Key = builder.Configuration["JWT_Secret:Key"];
JWT_Secret.Issuer = builder.Configuration["JWT_Secret:Issuer"];
JWT_Secret.Audience = builder.Configuration["JWT_Secret:Audience"];

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.CreateMap<ComplaintDto, Complaint>();
    mc.CreateMap<Complaint, ComplaintDto>();
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddCors(policy => {
    policy.AddPolicy("OpenCorsPolicy", opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("OpenCorsPolicy");
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
