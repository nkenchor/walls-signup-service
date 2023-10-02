using Walls_SignUp_Service.Domain;
using Serilog;
using System.Text.Json;


namespace Walls_SignUp_Service.Api;


public class SignUpService: ISignUpService
{ 
   
    readonly ISignUpEventService _eventBus;
    readonly ISignUpRepository _signUpRepository;
    readonly ISignUpValidationService _signUpValidatorService;
    

    public SignUpService(
    ISignUpEventService eventBus, ISignUpRepository signUpRepository, ISignUpValidationService signUpValidatorService) 
    {
        _signUpRepository = signUpRepository;
        _signUpValidatorService = signUpValidatorService;
        _eventBus = eventBus;
    }

    public async Task<SignUpDto> GetSignUpByReference(string reference)
    {
        try
        {
            Log.Information("Getting data by reference {0}", reference);
          
            var signUp = await _signUpRepository.GetSignUpByReference(reference);
            if (signUp == null)
            {
            Log.Error($"Error getting signup with reference: {reference}");
                throw new AppException(new[]{$"Error getting sign up with reference: {reference}"},"DATA_NOT_FOUND",404);
            
            }
           
            var SignUpDto = new SignUpDto(signUp);
            return SignUpDto;
        }
        catch (AppException e)
        {
             Log.Error($"Error Getting Sign Up: {e.Message}" );
            throw;
        }
        catch (Exception e)
        {
            Log.Error("Error Getting SignUp: {0}", e.Message );
            throw new AppException(new[]{e.Message}, "DATABASE",500);
        }
    }
    public async Task<SignUpDto> GetSignUpByUserReference(string user_reference)
    {
        try
        {
            Log.Information("Getting data by user reference {0}", user_reference);
          
            var signUp = await _signUpRepository.GetSignUpByUserReference(user_reference);
            if (signUp == null)
            {
            Log.Error($"Error getting signup with reference: {user_reference}");
                throw new AppException(new[]{$"Error getting sign up with user reference: {user_reference}"},"DATA_NOT_FOUND",404);
            
            }
           
            var SignUpDto = new SignUpDto(signUp);
            return SignUpDto;
        }
        catch (AppException e)
        {
             Log.Error($"Error Getting Sign Up: {e.Message}" );
            throw;
        }
        catch (Exception e)
        {
            Log.Error("Error Getting SignUp: {0}", e.Message );
            throw new AppException(new[]{e.Message}, "DATABASE",500);
        }
    }
    public async Task<ConfirmSignUpDto> GetConfirmedSignUpByReference(string reference)
    {
        try
        {
            Log.Information("Getting data by reference {0}", reference);
          
            var signUp = await _signUpRepository.GetSignUpByReference(reference);
            if (signUp == null)
            {
            Log.Error($"Error getting signup confirmation with reference: {reference}");
                throw new AppException(new[]{$"Error getting sign up confirmation with user reference: {reference}"},"DATA_NOT_FOUND",404);
            
            }
           
            var confirmedSignUpDto = new ConfirmSignUpDto{
                 User_Reference = reference,
                 Phone = signUp.Phone,
                 Is_Confirmed = signUp.Is_Confirmed
            };
            return confirmedSignUpDto;
        }
        catch (AppException e)
        {
             Log.Error($"Error Getting Sign Up: {e.Message}" );
            throw;
        }
        catch (Exception e)
        {
            Log.Error("Error Getting SignUp: {0}", e.Message );
            throw new AppException(new[]{e.Message}, "DATABASE",500);
        }
    }
    public async Task<AppResponse> ConfirmSignUp(string reference, ConfirmSignUpDto confirmSignUpDto)
    {
         // Retrieve the signUp from the repository
        var signUp = GetSignUpByReference(reference).Result.SignUp;
        //check if current device is = registed device
        var check = await IsRegisteredDevice(confirmSignUpDto.Device,signUp.Device);
        if(!await IsRegisteredDevice(confirmSignUpDto.Device,signUp.Device))
        {
             Log.Error($"Error getting registered device: {confirmSignUpDto.Device.Model}");
                throw new AppException(new[]{$"Sorry, device not registered. Please stand advised."},"DEVICE_NOT_FOUND",401);
            
        }
    
        // Confirm the signUp
        signUp.Is_Confirmed = true;
        signUp.Updated_On = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK");

        await _signUpRepository.UpdateSignUp(reference, signUp);
            
        Log.Information("Data Updated");
        var response = new AppResponse(string.Format(MessageProvider.SignUpConfirmSuccessful,reference),
        new[]{reference},"SIGNUP_CONFIRM_SUCCESS",202) ;
        // Save the updated signUp to the repository
        return response;
    }
    public async Task<AppResponse> ValidateContact(string reference, ValidateContactDto dto)
    {
        try
        {
            var signUp = GetSignUpByReference(reference).Result.SignUp;

            //check if current device is = registed device

            if(!await IsRegisteredDevice(dto.Device,signUp.Device))
            {
             Log.Error($"Error getting registered device: {dto.Device.Model}");
                throw new AppException(new[]{$"Sorry, device not registered. Please stand advised."},"DEVICE_NOT_FOUND",401);
            
            }

             Log.Information("Publishing validateOtp Event");
            //publish the event to the channel
            await _eventBus.PublishValidateContactEvent(dto);
            Log.Information("Event Published");
            var reponse = new AppResponse(MessageProvider.OtpSentSuccessful,
            new[]{dto.Reference},"VALIDATE_NEW_CONTACT_SENT_SUCCESS",201) ;
           

            return reponse;
        }
        catch (AppException e)
        {
             Log.Error($"Error Creating Sign Up: {e.Message}" );
            throw;
        }
        catch (Exception e)
        {
          
            Log.Error($"Error Creating SignUp: {e.Message}" );
            throw new AppException(new[]{e.Message},  "DATABASE",500);
        }

    }
    public async Task<AppResponse> CreateNewContact(string reference, NewContactDto dto)
    {
        var signUp = GetSignUpByReference(reference).Result.SignUp;

        if(!await IsRegisteredDevice(dto.Device,signUp.Device))
        {
             Log.Error($"Error getting registered device: {dto.Device.Model}");
                throw new AppException(new[]{$"Sorry, device not registered. Please stand advised."},"DEVICE_NOT_FOUND",401);
            
        }
        Log.Information("Publishing new contact Event");
            //publish the event to the channel
        await _eventBus.PublishContactEvent(dto);
        Log.Information("Event Published");
          var reponse = new AppResponse(MessageProvider.OtpSentSuccessful,
            new[]{dto.Reference},"NEW_CONTACT_CREATE_SUCCESS",201) ;

        return reponse;
    }

    public async Task<AppResponse> CreateSignUp(CreateSignUpDto signUpDto)
    {
        try
        {
            Log.Information("Validating input data {0} ...", signUpDto);
            var validationError = _signUpValidatorService.ValidateCreateSignUp(signUpDto);
            if (validationError!= null) 
            {  
                Log.Error($"Error validating input data: {JsonSerializer
                .Serialize(signUpDto)}.  Error: {validationError.ErrorData}");
                throw new AppException(validationError.ErrorData);
            }

            Log.Information("Mapping SignUp Data");
            var signUp = new SignUp(signUpDto);
            Log.Information("Inserting SignUp Data");
            //Save to the mongo database
            await _signUpRepository.CreateSignUp(signUp);
            Log.Information("Data Inserted");

            var reponse = new AppResponse(string.Format(MessageProvider.SignUpSuccessful,signUp.Reference),
            new[]{signUp.Reference.ToString()},"SIGNUP_SUCCESS",201) ;

            Log.Information("Publishing Sign up Event");
            //publish the event to the channel
            await _eventBus.PublishSignUpCreatedEvent(signUp);
            Log.Information("Event Published");

            //Send Otp
            var createSignUpOtpDto = new NewContactDto{
                Reference = signUp.Reference,
                User_Reference = signUp.User_Reference,
                Phone = signUp.Phone,
                Device = signUp.Device
            };
            //Create and publish the new contact
            await CreateNewContact(signUp.Reference,createSignUpOtpDto);
           

            return reponse;
        }
        catch (AppException e)
        {
             Log.Error($"Error Creating Sign Up: {e.Message}" );
            throw;
        }
        catch (Exception e)
        {
          
            Log.Error($"Error Creating SignUp: {e.Message}" );
            throw new AppException(new[]{e.Message},  "DATABASE",500);
        }
    }
    async Task<bool> IsRegisteredDevice(Device currentDevice, Device registeredDevice)
    {
        Log.Information("Validating Device");
       
        return  await Task.FromResult(currentDevice.Imei == registeredDevice.Imei
        && currentDevice.Type == registeredDevice.Type
        && currentDevice.Brand == registeredDevice.Brand
        && currentDevice.Model == registeredDevice.Model
        && currentDevice.Device_Reference == registeredDevice.Device_Reference);
    }
}

