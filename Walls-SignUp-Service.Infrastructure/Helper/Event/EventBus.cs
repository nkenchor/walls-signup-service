
using Walls_SignUp_Service.Domain;
using StackExchange.Redis;
using Serilog;
using System.Text.Json;
using System.Reflection;

namespace Walls_SignUp_Service.Infrastructure;

public class EventBus: IEventBus
{ 
   
    readonly IEBProvider _ebProvider;
    readonly IConnectionMultiplexer _connection;
   
    public EventBus(IEBProvider provider)
    {
        _ebProvider = provider;
        _connection = _ebProvider.Connect();

    }
    public async Task Publish<TEvent>(TEvent _event)
    {
        var db = _connection.GetSubscriber();

        // Serialize the event to JSON using System.Text.Json
        string serializedEvent = JsonSerializer.Serialize(_event);

        // Publish the event to a Redis channel
        Log.Information($"Publishing event: {serializedEvent}" );
        var name = typeof(TEvent).Name.ToUpper();
        await db.PublishAsync(typeof(TEvent).Name.ToUpper(), serializedEvent,flags: CommandFlags.FireAndForget);
        
        
    }

    public async Task Subscribe<TEvent>(Func<TEvent, Task> handler)
    {
        var subscriber = _connection.GetSubscriber();
        Log.Information($"Subscribing to event: {typeof(TEvent).Name.ToLower()}");
        await subscriber.SubscribeAsync(typeof(TEvent).Name.ToUpper(), async (channel, message) =>
        {
                Log.Information($"Deserializing event: {message}");
                var deserializedEvent = JsonSerializer.Deserialize<TEvent>(message.ToString());
                Log.Information($"Deserialized event: {deserializedEvent}");
                // Invoke the event handler with the deserialized event
                if (deserializedEvent !=null)
                    await handler(deserializedEvent);
     
        });
    }

    
}