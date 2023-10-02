using System.ComponentModel.DataAnnotations;
using FluentValidation.Validators;

namespace Walls_SignUp_Service.Domain;

public interface ISignUpValidationService{
      
      AppException ValidateCreateSignUp(CreateSignUpDto createSignUpDto);
      AppException ValidateConfirmSignUp(ValidateContactDto confirmSignUpDto);
      
}

