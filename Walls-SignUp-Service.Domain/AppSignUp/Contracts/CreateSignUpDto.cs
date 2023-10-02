using System;
using System.Diagnostics;
using System.Linq;



namespace Walls_SignUp_Service.Domain;
public class CreateSignUpDto
{ 
   public string Phone { get; set; }
   public Device Device{get; set; }
   public Location Location{get; set; }


}