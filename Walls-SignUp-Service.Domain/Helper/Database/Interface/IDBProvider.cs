using System.DirectoryServices.Protocols;
using System.Net;
using MongoDB.Driver;
namespace Walls_SignUp_Service.Domain;
public interface IDBProvider
   {
      public IMongoDatabase Connect();
      public int GetPageLimit();
   }