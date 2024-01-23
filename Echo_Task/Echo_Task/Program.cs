using Domain.Security;
using Infrastructure.Refit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Refit;
using Repositories.Data;
using Repositories.IRepositories;
using Repositories.Repositories;
using Services.IServices;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddMvc();

ConfigurationManager Configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

builder.Services.AddDbContext<EchoTaskDBContext>(options => {
    // Our DATABASE_URL from js days
    string connectionString = Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<EchoTaskDBContext>()
        .AddDefaultTokenProviders();

builder.Services.AddHttpClient<IComplaintAPI>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7125/api/ComplaintApi");
}).AddTypedClient(client => RestService.For<IComplaintAPI>(client));

builder.Services.AddHttpClient<IDemandAPI>(client =>
{
    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
}).AddTypedClient(client => RestService.For<IDemandAPI>(client));

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserSvc, UserSvc>();
builder.Services.AddScoped<IComplaintRepo, ComplaintRepo>();
builder.Services.AddScoped<IComplaintSvc, ComplaintSvc>();
builder.Services.AddScoped<IDemandRepo, DemandRepo>();
builder.Services.AddScoped<IDemandSvc, DemandSvc>();

JWT_Secret.Key = builder.Configuration["JWT_Secret:Key"];
JWT_Secret.Issuer = builder.Configuration["JWT_Secret:Issuer"];
JWT_Secret.Audience = builder.Configuration["JWT_Secret:Audience"];

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
