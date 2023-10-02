using System.DirectoryServices.Protocols;
using System.Net;
using StackExchange.Redis;
using System.Text.Json;

namespace Walls_SignUp_Service.Domain;
public interface IEventBus
{
     Task Publish<TEvent>(TEvent @event);
     Task Subscribe<TEvent>(Func<TEvent, Task> handler);
}
