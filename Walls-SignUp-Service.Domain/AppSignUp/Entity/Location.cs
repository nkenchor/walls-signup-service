using System;
using System.Diagnostics;
using System.Linq;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace Walls_SignUp_Service.Domain;
public class Location
{ 
  
   public double Latitude { get; set; }
   public double Longitude { get; set; }

   public Location(double latitude, double longitude)
   {
      Latitude = latitude;
      Longitude = longitude;
   }

}