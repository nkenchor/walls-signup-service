
using System;
using System.Threading.Tasks;
using Walls_SignUp_Service.Domain;

namespace Walls_SignUp_Service.Api;

    public class SignUpEventHandler: ISignUpEventHandler
    {
      
        readonly ISignUpService _signupService ; 
        
        public SignUpEventHandler(ISignUpService signupService) {
            _signupService = signupService;
           
        }
        public async Task HandleContactValidatedEvent(OtpValidatedEvent otpValidatedEvent)   
        {

            // Now you can access the data in the object
            var otpValidatedEventData = otpValidatedEvent.EventData;
        

            // Handle the OTP success event in other ways...
            var reference = otpValidatedEventData.Reference;
            var userReference = otpValidatedEventData.User_Reference;
            var phone = otpValidatedEventData.Contact;
            var device = otpValidatedEventData.Device;

            var confirmSignUpDto = new ConfirmSignUpDto{
                Reference = reference,
                User_Reference = userReference,
                Phone = phone,
                Is_Confirmed = true
            };

            await _signupService.ConfirmSignUp(reference,confirmSignUpDto);
        }

   
}
