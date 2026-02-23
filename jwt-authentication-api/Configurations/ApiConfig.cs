namespace API.Configurations;

public static class ApiConfig
{
    public static WebApplicationBuilder AddApiConfig(this WebApplicationBuilder builder)
    {
        _ = builder.Services.AddControllers();

        return builder;
    }
}