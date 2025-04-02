using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopAppApi.Data;
using ShopAppApi.Helpers;
using ShopAppApi.Helpers.Interfaces;
using ShopAppApi.Middlewares;
using ShopAppApi.Repositories.Auth;
using ShopAppApi.Repositories.CartRepo;
using ShopAppApi.Repositories.Categories;
using ShopAppApi.Repositories.Menus;
using ShopAppApi.Repositories.Metrics;
using ShopAppApi.Repositories.Products;
using ShopAppApi.Repositories.RedisCache;
using ShopAppApi.Repositories.RepoCustomer;
using ShopAppApi.Repositories.TelegramBotRepository;
using StackExchange.Redis;
using System.Text;
using System.Text.Json.Serialization;

var MyAllowSpecificOrigins = "_MyAllowSubdomainPolicy";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ShopAppContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DockerData"));
});

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<ITaxRepository, TaxRepository>();
builder.Services.AddScoped<ITelegramRepository, TelegramRepository>();
builder.Services.AddScoped<IRedisCache, RedisCache>();
builder.Services.AddScoped<IFileHelper, FileHelper>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IStringHelper, StringHelper>();
builder.Services.AddScoped<ICartRepository, CartRepository>();

builder.Services.AddSingleton<ICoreMonitoringData, InfluxData>();

builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = $"{builder.Configuration["RedisCache:Host"]}:{builder.Configuration["RedisCache:Port"]}";
            options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions()
            {
                AbortOnConnectFail = true,
                EndPoints = { options.Configuration ?? throw new ArgumentNullException("RedisDb connection string is null") }
            };
        });
// builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect($"{builder.Configuration["RedisCache:Host"]}:{builder.Configuration["RedisCache:Port"]}"));

builder.Services.AddHttpContextAccessor();

// builder.Services.AddHostedService<Worker>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowedToAllowWildcardSubdomains();
                      });
});

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddControllers().AddJsonOptions(x =>
  {
      x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
      x.JsonSerializerOptions.WriteIndented = true;
  });

var influxUrl = $"{builder.Configuration["InfluxSettings:Host"]}?org={builder.Configuration["InfluxSettings:Org"]}&bucket={builder.Configuration["InfluxSettings:Bucket"]}&token={builder.Configuration["InfluxSettings:Token"]}";
builder.Services.AddHealthChecks()
    .AddInfluxDB(influxUrl, name: "influx-database", tags: ["influxdb"]);

// builder.Services.AddScheduler();
// builder.Services.AddTransient<HealthCheckJobInvocable>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.SaveToken = true;
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key")))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// app.Services.UseScheduler(scheduler =>
// {
//     scheduler.Schedule<HealthCheckJobInvocable>().EveryThirtySeconds().PreventOverlapping(nameof(HealthCheckJobInvocable));
// });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseHttpsRedirection();
}
// app.UseHttpsRedirection();

app.UseMiddleware<GlobalRoutePrefixMiddleware>("/api/v1");
app.UsePathBase(new PathString("/api/v1"));

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

// Register routes directly at the top level
app.MapControllers();
// Add other route mappings here if needed
// app.MapGet("/", () => "Hello - " + System.Net.Dns.GetHostName());

app.Run();
