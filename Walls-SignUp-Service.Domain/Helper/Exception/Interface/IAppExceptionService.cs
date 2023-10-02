using System.DirectoryServices.Protocols;
using FluentValidation.Results;

namespace Walls_SignUp_Service.Domain;
public interface IAppExceptionService
   {
      AppException GetValidationExceptionResult(ValidationResult validationResult);
      
   }