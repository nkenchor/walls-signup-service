namespace Walls_SignUp_Service.Domain;

public static class MessageProvider
{
    public const string SerilogOutPutTemplate = "{{\"@timestamp\": \"{Timestamp:u}\", " +
                 "\"level\": \"[{Level}]\", \"status_code\": {StatusCode},  \"message\": \"{Message:lj}\"," + 
                 "\"time_elapsed\": {Elapsed}, \"source\": \"{SourceContext}\"," + 
                 "\"logger_name\": \"Serilog\", \"app_name\": \"{ApplicationName}\", \"version\": \"{Version}\", " + 
                 "\"request_method\": \"{RequestMethod}\", \"request_path\": \"{RequestPath}\"," + 
                 "\"x_correlation_id\": \"{CorrelationId}\", \"walls_signup_agent\": \"{ClientAgent}\"," + 
                 "\"client_name\": \"{MachineName}\", \"client_ip\": \"{ClientIp}\"}}{NewLine}";
    public const string MongoDBDown = "MongoDB Server is down";
    public const string RedisDown = "Redis Server is down";

    public const string UserNotFound = "User Not Found";
    public const string UserAlreadyExists = "User Already Exists";
    public const string UserEmailAlreadyExists = "User Email Already Exists";
    public const string UserCredentialInvalid  ="Invalid Credentials. Please stand advised";
    public const string InvalidParameters  ="Parameters must be in the required format and must not be null. Please stand advised.";
    public const string UserNameNull = "Username must not be empty. Please stand advised.";
    public const string PasswordNull = "Password must not be empty. Please stand advised.";

    public const string SignUpSuccessful = "{0} has been accepted, awaiting otp verification.";
    public const string SignUpConfirmSuccessful = "{0} has been verified successfully.";

    public const string OtpSentSuccessful = "Otp has been sent successfully.";
}