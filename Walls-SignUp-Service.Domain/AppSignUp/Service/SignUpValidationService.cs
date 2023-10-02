
namespace Walls_SignUp_Service.Domain;

public class SignUpValidationService:ISignUpValidationService
{
      
      public AppException ValidateCreateSignUp(CreateSignUpDto createSignUpDto) 
      {
        return new ErrorService().GetValidationExceptionResult(new CreateSignUpDtoValidator().Validate(createSignUpDto));
      }   
      public AppException ValidateConfirmSignUp(ValidateContactDto confirmSignUpDto) 
      {
        return new ErrorService().GetValidationExceptionResult(new ConfirmSignUpDtoValidator().Validate(confirmSignUpDto));
      } 
       
}