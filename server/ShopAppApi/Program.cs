using Coravel;
using Coravel.Scheduling.Schedule.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShopAppApi;
using ShopAppApi.Data;
using ShopAppApi.Helpers;
using ShopAppApi.Job;
using ShopAppApi.Middlewares;
using ShopAppApi.Repositories.Metrics;
using ShopAppApi.Repositories.Products;
using ShopAppApi.Repositories.RepoCustomer;
using System.Text.Json.Serialization;

var MyAllowSpecificOrigins = "_MyAllowSubdomainPolicy";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ShopAppContext>( option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DockerData"));
});

builder.Services.AddScoped<IStringHelper, StringHelper>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();

builder.Services.AddSingleton<ICoreMonitoringData, InfluxData>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddHostedService<Worker>();

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
  { x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
      x.JsonSerializerOptions.WriteIndented = true;
  });

  var influxUrl = $"{builder.Configuration["InfluxSettings:Host"]}?org={builder.Configuration["InfluxSettings:Org"]}&bucket={builder.Configuration["InfluxSettings:Bucket"]}&token={builder.Configuration["InfluxSettings:Token"]}";
builder.Services.AddHealthChecks()
    .AddInfluxDB(influxUrl, name: "influx-database", tags: ["influxdb"]);

builder.Services.AddScheduler();
builder.Services.AddTransient<HealthCheckJobInvocable>();



var app = builder.Build();

app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<HealthCheckJobInvocable>().EveryThirtySeconds().PreventOverlapping(nameof(HealthCheckJobInvocable));
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseHttpsRedirection();
}


app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<GlobalRoutePrefixMiddleware>("/api/v1");
app.UsePathBase(new PathString("/api/v1"));

app.MapGet("/", () => "Hello World!");

app.Run();
