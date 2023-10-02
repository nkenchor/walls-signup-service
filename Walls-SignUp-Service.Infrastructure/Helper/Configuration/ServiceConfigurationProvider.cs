using Walls_SignUp_Service.Domain;
using Serilog;
namespace Walls_SignUp_Service.Infrastructure;


public class ServiceConfigurationProvider
{ 
    
    public static void MapConfiguration(IConfiguration _configuration)
    {
         Log.Information("Mapping service configuration from env file to global configuration object...");
        ServiceConfiguration.Address=_configuration.GetSection("Service")["Address"];
        ServiceConfiguration.Port=_configuration.GetSection("Service")["Port"];
        ServiceConfiguration.Name=_configuration.GetSection("Service")["Name"];;
        ServiceConfiguration.Mode=_configuration.GetSection("Service")["Mode"];
        ServiceConfiguration.LogFileName=_configuration.GetSection("Service")["LogFileName"];
        ServiceConfiguration.LogDirectory=_configuration.GetSection("Service")["LogDirectory"];
        ServiceConfiguration.EventBusUrl=_configuration.GetSection("Service")["EventBusUrl"];
        ServiceConfiguration.LaunchUrl=_configuration.GetSection("Service")["LaunchUrl"];  
        ServiceConfiguration.TokenAudience = _configuration.GetSection("Service")["TokenAudience"];
        ServiceConfiguration.TokenIssuer = _configuration.GetSection("Service")["TokenIssuer"];
        ServiceConfiguration.TokenKey = _configuration.GetSection("Service")["TokenKey"];
        ServiceConfiguration.TokenExpiry = _configuration.GetSection("Service")["TokenExpiry"];
        
        
    }
    
    public static bool CheckConfiguration(string envFilePath)
    {
        Log.Information("Reading service configuration from env file...");
      
        if (!File.Exists(envFilePath)) 
        {
            Log.Error("Env file does not exist");
            return false;
        }
        foreach (var line in File.ReadAllLines(envFilePath))
        {
            var parts = line.Split(new[] {'='},2,StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2) continue;
            Log.Information("Setting global environment variable" + parts[0]);
            Environment.SetEnvironmentVariable(parts[0], parts[1]);
        }
        return true;
    }

    
}