using Walls_SignUp_Service.Domain;
using MongoDB.Driver;
using Serilog;
using System.Text.Json;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace Walls_SignUp_Service.Infrastructure;
public class SignUpRepository: ISignUpRepository
{ 
   
   
    readonly IDBProvider _dbProvider;
    readonly IMongoCollection<SignUp> _signupCollection; 
    


    public SignUpRepository(IDBProvider dbProvider) 
    {
     
        _dbProvider = dbProvider;
      
        
        _signupCollection =_dbProvider.Connect().GetCollection<SignUp>(nameof(SignUp).ToLower());

        // var indexKeysDefinition = Builders<SignUp>.IndexKeys.Ascending(signup => signup.Phone);
        // _signupCollection.Indexes.CreateOneAsync(new CreateIndexModel<SignUp>(indexKeysDefinition,
        // new CreateIndexOptions() { Unique = true }));
        
    }
    public async Task<string> CreateSignUp(SignUp  signUp)
    {
        try
        {
            Log.Information("Inserting Identity Data");
            await _signupCollection.InsertOneAsync(signUp);
        
            Log.Information("Data Inserted");

            return signUp.Reference;
        }
        catch (AppException e)
        {
             Log.Error($"Error Creating SignUp: {e.Message}" );
            throw;
        }
        catch (Exception e)
        {
          
            Log.Error($"Error Creating SignUp: {e.Message}" );
            throw new AppException(new[]{e.Message},  "DATABASE",500);
        }
    }

  
    public async Task<SignUp> GetSignUpByReference(string reference)
    {
        try
        {
            Log.Information("Getting data by reference {0}", reference);
          
            return await _signupCollection.Find(signup => signup.Reference == reference).FirstOrDefaultAsync();
           
            
        }
        catch (Exception e)
        {
            Log.Error("Error Getting SignUp: {0}", e.Message );
            throw new AppException(new[]{e.Message}, "DATABASE",500);
        }
    }
    public async Task<SignUp> GetSignUpByUserReference(string user_reference)
    {
        try
        {
            Log.Information("Getting data by user eference {0}", user_reference);
          
            return await _signupCollection.Find(signup => signup.User_Reference == user_reference).FirstOrDefaultAsync();
           
            
        }
        catch (Exception e)
        {
            Log.Error("Error Getting SignUp: {0}", e.Message );
            throw new AppException(new[]{e.Message}, "DATABASE",500);
        }
    }
    public async Task<string> UpdateSignUp(string reference, SignUp signUp)
    {
         try
        {
            Log.Information("Updating database {0}", reference);
        await _signupCollection.ReplaceOneAsync(signup => signup.Reference == reference, signUp);
            
        return reference;
           
        }
        catch (AppException e)
        {
             Log.Error($"Error Confirming SignUp: {e.Message}" );
            throw;
        }
        catch (Exception e)
        {
            Log.Error("Error Confirming SignUp: {0}", e.Message );
            throw new AppException(new[]{e.Message}, "DATABASE",500);
        }
    }


    
}