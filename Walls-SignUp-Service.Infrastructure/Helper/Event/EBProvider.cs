
using Walls_SignUp_Service.Domain;
using StackExchange.Redis;
using Serilog;

namespace Walls_SignUp_Service.Infrastructure;

public class EBProvider: IEBProvider
{ 
    readonly IConfiguration _configuration;
    readonly EBConnection _connection;
   
    public EBProvider(IConfiguration configuration, EBConnection connection)
    {
        
        _connection = connection;
        _configuration = configuration;
        _configuration.GetSection(nameof(EBConnection)).Bind(_connection = connection); 
    }

    public IConnectionMultiplexer Connect()
    {
      
            Log.Information("Connecting to Redis...");
            var connection = ConnectionMultiplexer.Connect(_connection.ConnectionString);

         

            var connectionState = connection.IsConnected;
            if (!connectionState)
            {
                 Log.Error("Error connecting to Redis");
                 throw new  AppException(new[]{ $"Server Error: {MessageProvider.RedisDown}"}, "SERVER",500);
            }
           
            Log.Information("Connecting to Redis Server successful...");
            Log.Information("Getting database...");
            return connection;
    }

    
}