using System;
using System.Diagnostics;
using System.Linq;



namespace Walls_SignUp_Service.Domain;
public class ConfirmSignUpDto
{ 
   public string Reference { get; set; }
   public string User_Reference { get; set; }
   public string Phone { get; set; }
   public Device Device { get; set; }
   public bool Is_Confirmed { get; set; }

}