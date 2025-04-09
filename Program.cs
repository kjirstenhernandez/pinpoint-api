using api.Data;
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using api.Interfaces;
using api.Repository;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
builder.Services.AddControllers();
builder.Services.AddOpenApi();  // .NET 9.0 dropped support for swagger, OpenAPI being used for Scalar instead
builder.Services.AddControllers().AddNewtonsoftJson(options => {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;;
    });
    
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); // Connection for SQL Database
});

builder.Services.AddScoped<IUserRepository, UserRepository>(); // User Interface registration
builder.Services.AddScoped<IEventRepository, EventRepository>(); // Event Interface registration

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/api-reference"); // Scalar use instead of swagger, found at this alterenative was found at https://youtu.be/8yI4gD1HruY?si=hD56Xw5gNjQLpkcZ
}
else 
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();