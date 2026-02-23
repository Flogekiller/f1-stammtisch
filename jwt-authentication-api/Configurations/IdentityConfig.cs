using API.Data;
using Microsoft.AspNetCore.Identity;

namespace API.Configurations;

public static class IdentityConfig
{
    public static WebApplicationBuilder AddIdentityConfig(this WebApplicationBuilder builder)
    {
        _ = builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                        .AddRoles<IdentityRole>()
                        .AddEntityFrameworkStores<ApplicationDbContext>();

        return builder;
    }
}