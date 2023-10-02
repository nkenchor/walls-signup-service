using System;
using System.Diagnostics;
using System.Linq;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization.Attributes;
using Walls_SignUp_Service.Domain;
namespace Walls_SignUp_Service.Api;
public class OtpValidatedData
{ 

   public required string Reference { get; set; }
   public required string User_Reference{get;set; }
   public required string Contact{get;set;}
   public required Device Device { get; set; }
   

   

}