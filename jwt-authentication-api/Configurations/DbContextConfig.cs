using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Configurations;

public static class DbContextConfig
{
    public static WebApplicationBuilder AddDbContextConfig(this WebApplicationBuilder builder)
    {
        string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        _ = builder.Services.AddDbContext<ApplicationDbContext>(options =>
                       options.UseSqlServer(connectionString));

        return builder;
    }
}