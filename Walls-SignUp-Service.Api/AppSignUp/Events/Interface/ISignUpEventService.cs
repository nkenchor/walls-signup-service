

using System.Threading.Tasks;
using Walls_SignUp_Service.Domain;

namespace Walls_SignUp_Service.Api;

public interface ISignUpEventService
{
    public Task PublishSignUpCreatedEvent(SignUp signUp);
    public Task PublishContactEvent(NewContactDto dto);
    public Task PublishValidateContactEvent(ValidateContactDto dto);
    public Task SubscribeToContactValidatedEvent(Func<OtpValidatedEvent, Task> handler);
}

