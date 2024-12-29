using Essence.Api;
using Essence.Api.Entries;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Supabase;

var options = new SupabaseOptions
{
    AutoRefreshToken = true,
    AutoConnectRealtime = true,
    // SessionHandler = new SupabaseSessionHandler() <-- This must be implemented by the developer
};

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        var supabaseConfig = new SupabaseConfiguration
        {
            Url = Environment.GetEnvironmentVariable("SUPABASE_URL", EnvironmentVariableTarget.Process) ?? string.Empty,
            Key = Environment.GetEnvironmentVariable("SUPABASE_KEY", EnvironmentVariableTarget.Process) ?? string.Empty
        };
        services.AddSingleton(provider => new Supabase.Client(supabaseConfig.Url ?? string.Empty, supabaseConfig.Key ?? string.Empty, options));
        services.AddScoped<IEntryRepository, EntryRepository>();
    })
    .Build();

host.Run();
