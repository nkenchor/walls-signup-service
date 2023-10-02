namespace Walls_SignUp_Service.Domain;

public interface IServiceProvider
{
    void MapConfig();
    void ReadConfig(string envFilePath);
    
}
    
