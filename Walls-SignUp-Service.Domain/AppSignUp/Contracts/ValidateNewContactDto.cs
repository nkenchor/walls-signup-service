using System;
using System.Diagnostics;
using System.Linq;



namespace Walls_SignUp_Service.Domain;
public class ValidateContactDto
{ 
   public string Reference { get; set; }
   public string User_Reference { get; set; }
   public string Otp{get;set;}
   public string Phone { get; set; }
   public Device Device { get; set; }


}