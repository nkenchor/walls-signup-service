using System;
using System.Threading.Tasks;
using Serilog;
using Walls_SignUp_Service.Domain;
using Walls_SignUp_Service.Infrastructure;
namespace Walls_SignUp_Service.Api;

    public class SignUpEventService: ISignUpEventService
    {
        readonly EventBus _eventBus;
        public SignUpEventService (EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task PublishSignUpCreatedEvent(SignUp signUp)   
        {
          
            
            Log.Information("Mapping sign up event to signedup data");

            var Eto = new SignUpCreatedEvent
            {
                EventName = typeof(SignUpCreatedEvent).Name,
                EventType = typeof(SignUpCreatedEvent).Name,
                EventUserReference = signUp.Reference,
                EventData = new SignUpCreatedData{
                                                    Reference = signUp.Reference,
                                                    User_Reference = signUp.User_Reference,
                                                    Phone = $"{signUp.Phone}",
                                                    Created_On = signUp.Created_On,
                                                    Device = signUp.Device
                }
            };
            Log.Information($"Publishing signed up event for user with mobile number {signUp.Phone}");
            await _eventBus.Publish(Eto);
            
        }
        public async Task PublishContactEvent(NewContactDto signUpOtpDto)    
        {
          
            
            Log.Information("Mapping sign up event to signedup data");

            var Eto = new NewContactEvent
            {
                EventName = typeof(NewContactEvent).Name,
                EventType = typeof(NewContactEvent).Name,
                EventUserReference = signUpOtpDto.User_Reference,
                EventData = new NewContactData{
                                                    Reference = signUpOtpDto.Reference,
                                                    User_Reference = signUpOtpDto.User_Reference,
                                                    Phone = $"{signUpOtpDto.Phone}",
                                                    Device = signUpOtpDto.Device
                }
            };
            Log.Information($"Publishing signed up event for user with mobile number {signUpOtpDto.Phone}");
            await _eventBus.Publish(Eto);
            
        }
        public async Task PublishValidateContactEvent(ValidateContactDto validateOtpDto)   
        {
          
            
            Log.Information("Mapping validate otp event to validate otp data");

            var Eto = new ValidateContactEvent
            {
                EventName = typeof(ValidateContactEvent).Name,
                EventType = typeof(ValidateContactEvent).Name,
                EventUserReference = validateOtpDto.User_Reference,
                EventData = new ValidateContactData{
                                                    Reference = validateOtpDto.Reference,
                                                    User_Reference = validateOtpDto.User_Reference,
                                                    Otp = validateOtpDto.Otp,
                                                    Phone = $"{validateOtpDto.Phone}",
                                                    Device = validateOtpDto.Device
                                                    
                }
            };
            Log.Information($"Publishing valiate otp event for user with mobile number {Eto.EventData.Phone}");
            await _eventBus.Publish(Eto);
            
        }

        public async Task SubscribeToContactValidatedEvent(Func<OtpValidatedEvent, Task> handler)
        {
            Log.Information("Subscribing to signed up event");
            await _eventBus.Subscribe<OtpValidatedEvent>(handler);
        }
    }
