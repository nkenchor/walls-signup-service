using FluentValidation;
using FluentValidation.Results;

namespace Walls_SignUp_Service.Domain;

public class ConfirmSignUpDtoValidator : AbstractValidator<ValidateContactDto>
{
    public ConfirmSignUpDtoValidator()
    {
         RuleFor(x => x.Reference).NotEmpty().WithMessage("Sign up reference is required.")
             .Must(IsValidGuid).WithMessage("Invalid SignUp Reference.");
        RuleFor(x => x.User_Reference).NotEmpty().WithMessage("User reference is required.")
             .Must(IsValidGuid).WithMessage("Invalid SignUp Reference.");
        RuleFor(x => x.Otp).NotEmpty().WithMessage("OTP is required.")
            .Matches(@"^\d{6}$").WithMessage("Invalid OTP. OTP must be a 6-digit number.");
        RuleFor(x => x.Device).SetValidator(new DeviceValidator());
         RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+\d{1,3}\d{10,15}$").WithMessage("Invalid phone number. It must start with a dial code '+' followed by 1-3 digits and then 10-15 digits for the phone number.")
            .Custom((number, context) =>
            {
                if (number.StartsWith("+0"))
                {
                    // Remove the leading zero after the plus sign
                    string strippedNumber = number.Remove(1, 1);
                    context.MessageFormatter.AppendArgument("strippedNumber", strippedNumber);
                    context.AddFailure($"Leading zero after the dial code removed. Updated phone number: {strippedNumber}.");
                }
            });
           
           
        
    }
    private bool IsValidGuid(string guid)
    {
        return Guid.TryParse(guid, out _);
    }
    public override ValidationResult Validate(ValidationContext<ValidateContactDto> context)
    {
        return context.InstanceToValidate == null
            ? new ValidationResult(new[] { new ValidationFailure(nameof(ValidateContactDto), 
            "Parameters must be in the required format and must not be null. Please stand advised.") })
            : base.Validate(context);
    }
}  
