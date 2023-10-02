using Walls_SignUp_Service.Domain;

namespace Walls_SignUp_Service.Api;

public interface ISignUpEventHandler
{
    public  Task HandleContactValidatedEvent(OtpValidatedEvent otpValidatedEvent);
   
}
