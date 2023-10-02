using System;
using System.Diagnostics;
using System.Linq;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace Walls_SignUp_Service.Domain;
public class Device
{ 
  
   public string Device_Reference { get; set; }
   public string Imei { get; set; }
   public Options.DeviceType Type { get; set; }
   public string Brand{get;set;}
   public string Model { get; set; }

   public Device(string imei,  Options.DeviceType type, string brand, string model)
   {
      Device_Reference = Guid.NewGuid().ToString();
      Imei = imei;
      Type = type;
      Brand = brand;
      Model = model;
   }

}