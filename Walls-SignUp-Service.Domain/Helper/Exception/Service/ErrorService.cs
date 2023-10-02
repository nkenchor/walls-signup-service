
using FluentValidation.Results;
using Serilog;
namespace Walls_SignUp_Service.Domain;
public class ErrorService:IAppExceptionService
{
    public AppException GetValidationExceptionResult(ValidationResult validationResult)
    {
        
        
        if (!validationResult.IsValid)
        {
            Log.Information("Compiling validation errors...");
            
            string[] errorMessage = {};
            foreach (ValidationFailure failure in validationResult.Errors)
            {
                errorMessage = errorMessage.Append(failure.ErrorMessage).ToArray();
            }
            return new AppException(errorMessage);
        }
        return null;
        
    }
   
}