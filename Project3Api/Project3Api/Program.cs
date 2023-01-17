using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Console;
using Microsoft.IdentityModel.Tokens;
using Project3Api.Core;
using Project3Api.Core.Configuration;
using Project3Api.Core.Services;
using Project3Api.Data;
using Project3Api.Middlewares;
using Project3Api.Services;
using Project3Api.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// logger
builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole(options =>
{
    options.ColorBehavior = LoggerColorBehavior.Enabled;
});

builder.Services.AddControllers();

// validations for user input
builder.Services.AddFluentValidation(validation =>
{
    validation.RegisterValidatorsFromAssemblyContaining<DeskReservationValidator>();
    validation.RegisterValidatorsFromAssemblyContaining<DeskValidator>();
    validation.RegisterValidatorsFromAssemblyContaining<UserValidator>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS configuration
if (builder.Environment.IsProduction())
{
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            policy.WithOrigins(new[] { "http://20.114.173.131:80", "https://20.114.173.131:443" }) // TODO: make https only
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
    });
}
else
{
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            policy.WithOrigins(new[] { "https://localhost:4200", "http://localhost:4200", 
                                       "http://20.114.173.131:80", "https://20.114.173.131:443" })
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
    });
}

// database context
string dbConnectionString = "DefaultDB";
if (builder.Environment.IsProduction())
{
    dbConnectionString = "ProductionDB";
}

builder.Services.AddDbContext<ProjectDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(dbConnectionString));
});

// DI services
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITokenReaderService, TokenReaderService>();
builder.Services.AddScoped<IDeskReadService, DeskReadService>();
builder.Services.AddScoped<IDeskWriteService, DeskWriteService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IAllocationService, AllocationService>();
builder.Services.AddScoped<ILogService, LogService>();


// authentication service
string authorityServer = "https://localhost:9999";
string authentication = "DevAuthentication";
if (builder.Environment.IsProduction())
{
    authorityServer = "https://project3-identity-server.azurewebsites.net";
    authentication = "ProdAuthentication";
}

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = authorityServer;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration[$"{authentication}:Issuer"],
        };
    });

// authorization service
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ReadApi", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "office.read");
    });

    options.AddPolicy("UsersApi", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole(AuthorizationPolicyHelper.USER_ROLE);
        policy.RequireClaim("scope", "office.users");
    });

    options.AddPolicy("AdminApi", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole(AuthorizationPolicyHelper.ADMIN_ROLE);
        policy.RequireClaim("scope", "office.admin");
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsProduction())
{
    app.UseCors();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

// custom midlleware for blocking sesitive data in case of an 500: Internal Server Error
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
