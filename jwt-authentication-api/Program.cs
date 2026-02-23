using API.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Your extension methods (assuming they add services correctly)
builder.AddApiConfig()
       .AddJwtConfig()
       .AddDbContextConfig()
       .AddIdentityConfig()
       .AddSwaggerConfig();   // Swagger usually doesn't need to be before CORS

// Add controllers
builder.Services.AddControllers();

// ── CORS ── (only define once here – remove manual AddCors if AddCorsConfig already does it)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();   // important for cookies / auth headers if used
    });

    // Optional: production policy (restrict origins!)
    options.AddPolicy("Production", policy =>
    {
        policy.WithOrigins("https://your-frontend-domain.com")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// ── Middleware pipeline ── IMPORTANT ORDER!
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Use the dev policy in development
    app.UseCors("AllowAngularDev");
}
else
{
    // Use production policy (or stricter) in prod
    app.UseCors("Production");
}

// Common middleware – CORS should already be above auth!
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();