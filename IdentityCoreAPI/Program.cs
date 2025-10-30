
using IdentityCoreAPI;
using IdentityCoreAPI.Data;
using IdentityCoreAPI.Extentions;
using IdentityCoreAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(op =>
op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGenJwtAuth();  // AddSwaggerGen exist here in AddSwaggerGenJwtAuth
builder.Services.AddCustomJwt(builder.Configuration);// AddAuthentication exist here in AddCustomJwt



//builder.Services.AddAuthentication();   //authorize by token
builder.Services.AddAuthorization();     // login authorize
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // عنوان تطبيق Next.js أثناء التطوير
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // إذا كنت تستخدم Cookies أو Authorization header
    });
});

builder.Services.AddIdentityServer()
    .AddInMemoryClients(Config.Clients)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddTestUsers(Config.Users)
    .AddDeveloperSigningCredential();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowFrontend");


app.UseRouting();
//app.UseAuthentication();
//app.UseAuthorization();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


//using IdentityServerHost;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddIdentityServer()
//    .AddInMemoryClients(Config.Clients)
//    .AddInMemoryApiScopes(Config.ApiScopes)
//    .AddInMemoryApiResources(Config.ApiResources)
//    .AddInMemoryIdentityResources(Config.IdentityResources)
//    .AddTestUsers(Config.Users)
//    .AddDeveloperSigningCredential();

//var app = builder.Build();
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//app.UseIdentityServer();
//app.Run();
