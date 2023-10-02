using System.DirectoryServices.Protocols;
using System.Net;
using StackExchange.Redis;
using System.Text.Json;

namespace Walls_SignUp_Service.Domain;
public interface IEBProvider
{
   public IConnectionMultiplexer Connect();
}
