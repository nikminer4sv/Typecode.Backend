using Identity.WebApi;
using Identity.WebApi.Data;
using Identity.WebApi.Interfaces;
using Identity.WebApi.Models;
using Identity.WebApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSqlLocal"));
});

builder.Services.AddHttpClient();
builder.Services.AddScoped<IProfileService, ProfileService>();

builder.Services.AddCors(o => o.AddPolicy("AllPolicy", policy =>
{
    policy.AllowAnyOrigin(); 
    policy.AllowAnyMethod();
    policy.AllowAnyHeader();
}));

builder.Services.AddIdentity<User, IdentityRole>(config =>
    {
        config.Password.RequiredLength = 4;
        config.Password.RequireDigit = false;
        config.Password.RequireNonAlphanumeric = false;
        config.Password.RequireUppercase = false;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer(options =>
    {
        options.IssuerUri = "http://auth:5171";
    })
    .AddAspNetIdentity<User>()
    .AddInMemoryApiResources(Configuration.ApiResources)
    .AddInMemoryIdentityResources(Configuration.IdentityResources)
    .AddInMemoryApiScopes(Configuration.ApiScopes)
    .AddInMemoryClients(Configuration.Clients)
    .AddDeveloperSigningCredential();

builder.Services.AddAuthentication("Bearer")
    .AddIdentityServerAuthentication(options =>
    {
        options.Authority = "http://auth:5171";
        options.RequireHttpsMetadata = false;
        options.ApiName = "TypecodeApi";
        options.ApiSecret = "a75a559d-1dab-4c65-9bc0-f8e590cb388d";
    });

builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "Typecode.Auth.Cookie";
    config.LoginPath = "/auth/login";
    config.LogoutPath = "/auth/logout";
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


using(var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<AppDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception exception)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception, "An error occurred while app initialization");
    }
}

app.UseCors("AllPolicy");
app.UseRouting();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();