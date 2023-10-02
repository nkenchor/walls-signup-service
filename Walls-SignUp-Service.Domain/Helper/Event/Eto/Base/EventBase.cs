using System;

namespace Walls_SignUp_Service.Domain;

public abstract class EventBase<T>
{
    public string EventReference { get; set; } = Guid.NewGuid().ToString();
    public string EventName { get; set; } 
    public string EventDate { get;  set; }  =DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK");
    public string EventType { get; set; }
    public string EventSource { get; set; } = ServiceConfiguration.Name;
    public string EventUserReference { get; set; }

    public T EventData { get; set; }
    
}


   