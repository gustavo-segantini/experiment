using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Bank.Repository;
using Bank.Services;
using MediatR;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Bank;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddControllers()
            .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        });

        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

        services.AddSingleton<IAccountRepository, AccountRepository>();
        services.AddSingleton<IBalanceService, BalanceService>();

        services.AddSwaggerGen(c =>
        {
            c.UseInlineDefinitionsForEnums();
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bank API", Version = "v1" });
        });

        services.AddMediatR(Assembly.GetExecutingAssembly());
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bank API V1");
            c.RoutePrefix = string.Empty;
        });

        app.UseSerilogRequestLogging(c =>
        {
            c.IncludeQueryInRequestPath = true;
        }); // Enable Serilog request logging

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}