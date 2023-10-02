
using Walls_SignUp_Service.Domain;
namespace Walls_SignUp_Service.Api;
public class SignUpCreatedData
{ 

   public required string Reference { get; set; }
   public required string User_Reference { get; set; }
   public required string Phone { get; set; }
   public required string Created_On { get; set; }
   public required Device Device{get; set; }

   

}