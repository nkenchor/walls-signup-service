namespace Walls_SignUp_Service.Domain;

public interface ISignUpRepository{

    #region Database
    Task<string> CreateSignUp(SignUp signUp);

    Task<SignUp> GetSignUpByReference(string reference);

    Task<SignUp> GetSignUpByUserReference(string reference);
    
    Task<string> UpdateSignUp(string reference, SignUp signUp);

    #endregion
}

