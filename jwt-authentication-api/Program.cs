using API.Configurations;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddApiConfig()
       .AddCorsConfig()
       .AddSwaggerConfig()
       .AddJwtConfig()
       .AddDbContextConfig()
       .AddIdentityConfig();

IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
    _ = app.UseCors("Development");
}
else
{
    _ = app.UseCors("Production");
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
