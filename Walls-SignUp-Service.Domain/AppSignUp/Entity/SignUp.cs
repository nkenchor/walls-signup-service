using System;
using System.Diagnostics;
using System.Linq;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization.Attributes;
namespace Walls_SignUp_Service.Domain;
public class SignUp
{ 
  [BsonId]
   public string Reference { get; set; }
   public string User_Reference{get;set;}
   public string Phone { get; set; }
   public bool Is_Confirmed{get;set;}
   public Device Device{get; set; }
   public Location Location{get; set; }
   public string Created_On { get; set; }
   public string Updated_On { get; set; }
 


   public SignUp(CreateSignUpDto signup)
   {
      Reference = Guid.NewGuid().ToString();
      User_Reference = Guid.NewGuid().ToString();
      Phone = signup.Phone;
      Is_Confirmed = false;
      Device = signup.Device;
      Location = signup.Location;
      Created_On = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK");
      Updated_On = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK");
   }

}