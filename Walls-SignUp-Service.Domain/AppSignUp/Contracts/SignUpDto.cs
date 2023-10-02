using System;
using System.Diagnostics;
using System.Linq;



namespace Walls_SignUp_Service.Domain;
public class SignUpDto
{ 
   public SignUp SignUp{get;set;}

   public SignUpDto(SignUp signup)
   {
      SignUp = signup;
     
   }
   

}