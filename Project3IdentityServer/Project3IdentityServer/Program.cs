using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Console;
using Project3Api.Core.Repositories;
using Project3Api.Data;
using Project3Api.Data.Repositories;
using Project3IdentityServer.Configuration;
using Project3IdentityServer.var2;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

// Add services to the container.

// logger
builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole(options =>
{
    options.ColorBehavior = LoggerColorBehavior.Enabled;
});
builder.Logging.SetMinimumLevel(LogLevel.Debug);
builder.Logging.AddFilter("IdentityServer4", LogLevel.Debug);

builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:4200", "http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

string connString = "DefaultDB";
if (builder.Environment.IsProduction())
{
    connString = "ProductionDB";
}
builder.Services.AddDbContext<ProjectDbContext>(options =>
{
   options.UseSqlServer(builder.Configuration.GetConnectionString(connString));
});

builder.Services.AddTransient<IUserRepository, UserRepository>();

string isConnString = "ISProductionDB";
if (builder.Environment.IsProduction())
{
    isConnString = "ISProductionDB";
}
builder.Services.AddIdentityServer(options =>
{
    // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
    options.EmitStaticAudienceClaim = true;
})
    .AddDeveloperSigningCredential() // not recommended for production - you need to store your key material somewhere secure
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = c =>
        {
            c.UseSqlServer(builder.Configuration.GetConnectionString(isConnString),
                options => options.MigrationsAssembly(assembly));
        };
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = c =>
        {
            c.UseSqlServer(builder.Configuration.GetConnectionString(isConnString),
                options => options.MigrationsAssembly(assembly));
        };
    })
    .AddProfileService<ProfileService>();

var app = builder.Build();

// only migrate the initial data if no other data exists in the database
Config.MigrateInMemoryDataToSqlServer(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseCors();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();

