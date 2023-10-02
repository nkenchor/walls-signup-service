
using System.ComponentModel.DataAnnotations;
namespace Walls_SignUp_Service.Domain;
public static class ServiceConfiguration
{
    
    public static string Address { get; set; }
    public static string Port { get; set; }
    public static string Name{get; set; }
    public static string Mode{get; set; }
    public static string LogFileName{get; set; }
    public static string LogDirectory{get; set;}
    public static string EventBusUrl{get; set; }
    public static string LaunchUrl{get; set; }
    public static string TokenKey{get;set; }
    public static string TokenAudience{get;set; }
    public static string TokenIssuer{get;set; }
    public static string TokenExpiry{get;set;}
  
}

