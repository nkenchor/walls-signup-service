using System.Diagnostics;
using System.Linq;
using Walls_SignUp_Service.Domain;
namespace Walls_SignUp_Service.Api;
      


public class SignUpCreatedEvent:EventBase<SignUpCreatedData> { }
public class ValidateContactEvent:EventBase<ValidateContactData> { }
public class NewContactEvent:EventBase<NewContactData> { }