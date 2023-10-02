using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;

namespace Walls_SignUp_Service.Domain;


using FluentValidation;

public class CreateSignUpDtoValidator : AbstractValidator<CreateSignUpDto>
{
    public CreateSignUpDtoValidator()
    {
       
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
    
}





public class LocationValidator : AbstractValidator<Location>
{
    public LocationValidator()
    {
        RuleFor(x => x.Latitude).InclusiveBetween(-90, 90).WithMessage("Invalid latitude value. It must be between -90 and 90.");
        RuleFor(x => x.Longitude).InclusiveBetween(-180, 180).WithMessage("Invalid longitude value. It must be between -180 and 180.");

    }

}
public class DeviceValidator : AbstractValidator<Device>
{
    public DeviceValidator()
    {
        RuleFor(x => x.Device_Reference).NotEmpty().WithMessage("Device reference is required.")
                        .Must(IsValidGuid).WithMessage("Invalid Device Reference.");
        RuleFor(x => x.Imei).NotEmpty().WithMessage("IMEI number is required.")
            .Must(IsValidImei).WithMessage("Invalid IMEI number.");

        RuleFor(x => x.Type).IsInEnum().WithMessage("Invalid device type.");

        RuleFor(x => x.Brand).NotEmpty().WithMessage("Device brand is required.");

        RuleFor(x => x.Model).NotEmpty().WithMessage("Device model is required.");
    }
    private bool IsValidGuid(string guid)
    {
        return Guid.TryParse(guid, out _);
    }
    // IMEI validation
    private bool IsValidImei(string imei)
    {
        if (imei.Length != 15 || !long.TryParse(imei, out _))
        {
            return false;
        }

        int sum = 0;
        for (int i = 0; i < imei.Length; i++)
        {
            int digit = int.Parse(imei[i].ToString());
            if (i % 2 == 1)
            {
                digit *= 2;
                if (digit > 9)
                {
                    digit -= 9;
                }
            }
            sum += digit;
        }

        return sum % 10 == 0;
    }
}
