using Walls_SignUp_Service.Domain;

namespace Walls_SignUp_Service.Api;

public interface ISignUpService{

    #region Database
    Task<AppResponse> CreateSignUp(CreateSignUpDto signUpDto);
    Task<AppResponse> CreateNewContact(string reference, NewContactDto signUpOtpDto);
   
    Task<SignUpDto> GetSignUpByReference(string reference);
    Task<SignUpDto> GetSignUpByUserReference(string user_reference);

    Task<ConfirmSignUpDto> GetConfirmedSignUpByReference(string reference);
    
    Task<AppResponse> ValidateContact(string reference, ValidateContactDto validateOtpDto);

     Task<AppResponse> ConfirmSignUp(string reference, ConfirmSignUpDto confirmSignUpDto);
    
    #endregion
}

