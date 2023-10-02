

using System.Globalization;
namespace Walls_SignUp_Service.Domain;

public class AppResponse 
{
    public string Reference { get; set; }
    public string Message { get; set; }
    public string[] ResponseData { get; set; }
    public string ResponseType{get;set;}
    public int StatusCode { get; set; }
    public DateTime Timestamp { get; set; }
    public AppResponse(string message,string[] responseData, string responseType = "CREATE_SUCCESS", int statusCode  = 201) : base()
    {
      
        Reference = Guid.NewGuid().ToString();
        Message = message;
        ResponseData = responseData;
        ResponseType = responseType;
        StatusCode = statusCode;
        Timestamp = DateTime.Now;
    }

   
}


