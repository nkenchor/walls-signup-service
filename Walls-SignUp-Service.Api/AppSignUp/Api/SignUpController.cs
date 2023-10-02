
using Microsoft.AspNetCore.Mvc;
using Walls_SignUp_Service.Domain;

namespace Walls_SignUp_Service.Api;
    [ApiController]
    [Route("api/[controller]")]
    public class SignUpController : ControllerBase
    {
        readonly ISignUpService _signupService ; 
        
        public SignUpController(ISignUpService signupService) {
            _signupService = signupService;
           
        }
        [HttpPost("create-signup")]
        public async Task<ActionResult<string>> CreateSignUp([FromBody] CreateSignUpDto createSignUpDto)
        {
                return Ok (await _signupService.CreateSignUp(createSignUpDto));

        }
        [HttpPut("{reference}/validate-signup-otp")]
        public async Task<ActionResult<string>> ValidateOtp(string reference,[FromBody] ValidateContactDto validateOtpDto)
        {

                return Ok(await _signupService.ValidateContact(reference,validateOtpDto));

        }
         [HttpPut("{reference}/confirm-signup")]
        public async Task<ActionResult<string>> ConfirmSignUp(string reference,[FromBody] ConfirmSignUpDto confirmSignUpDto)
        {

                return Ok(await _signupService.ConfirmSignUp(reference,confirmSignUpDto));

        }
      
        [HttpGet("{reference}")]
        public async Task<ActionResult<SignUpDto>> GetSignUpByReference(string reference)
        {
  
                return Ok( await _signupService.GetSignUpByReference(reference));
           
        }
        [HttpGet("user-reference/{reference}")]
        public async Task<ActionResult<SignUpDto>> GetSignUpByUserReference(string reference)
        {
  
                return Ok( await _signupService.GetSignUpByReference(reference));
           
        }
        [HttpGet("{reference}/confirmation")]
        public async Task<ActionResult<SignUpDto>> GetConfirmedSignUpByReference(string reference)
        {
  
                return Ok( await _signupService.GetConfirmedSignUpByReference(reference));
           
        }
        
        [HttpGet("{reference}/create-signup-otp")]
        public async Task<ActionResult<SignUpDto>> CreateSignUpOtp(string reference, NewContactDto createSignUpOtpDto)
        {
  
                return Ok( await _signupService.CreateNewContact(reference, createSignUpOtpDto));
           
        }
     
    }

