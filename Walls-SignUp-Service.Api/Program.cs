using Microsoft.AspNetCore.Mvc;
using Walls_SignUp_Service.Domain;
using Serilog;
using Walls_SignUp_Service.Infrastructure;
using Walls_SignUp_Service.Api;

using Walls_SignUp_Service.Api.Extensions;
using serviceProvider = Walls_SignUp_Service.Infrastructure.ServiceConfigurationProvider;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

//Check for service configuration

if (serviceProvider.CheckConfiguration(@"../walls-signup-service.env") == false) return;

//Add environment variables to the global config
builder.Configuration.AddEnvironmentVariables();
//Map configuration to global class
serviceProvider.MapConfiguration(builder.Configuration);

// Suppress automatic model validation
builder.Services.Configure<ApiBehaviorOptions>(options =>{options.SuppressModelStateInvalidFilter = true;});

// Add Serilog
builder.Host.AddSerilog();


// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHeaderPropagation(options => options.Headers.Add("X-Correlation-Id"));
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSingleton<Walls_SignUp_Service.Infrastructure.ServiceConfigurationProvider>();
builder.Services.AddTransient<ISignUpRepository,SignUpRepository>();
builder.Services.AddTransient<ISignUpService,SignUpService>();
builder.Services.AddTransient<ISignUpEventService,SignUpEventService>();
builder.Services.AddTransient<ISignUpValidationService,SignUpValidationService>();
builder.Services.AddSingleton<IDBProvider,DBProvider>();
builder.Services.AddSingleton<DBConnection>();
builder.Services.AddSingleton<IEBProvider,EBProvider>();
builder.Services.AddSingleton<EBConnection>();
builder.Services.AddSingleton<IEventBus,EventBus>();
builder.Services.AddSingleton<EventBus>();

//Defining the service address
builder.WebHost.UseUrls($"{ServiceConfiguration.Address}:{ServiceConfiguration.Port}");

var app = builder.Build();


// Retrieve an instance of the user service
var signUpService = app.Services.GetRequiredService<ISignUpService>();
//instantiate the handler
var otpValidatedHandler = new SignUpEventHandler(signUpService);



// Retrieve an instance of the Redis subscriber service
var redisSubscriberService = app.Services.GetRequiredService<ISignUpEventService>();

// Call the SubscribeAndHandleEvent method to start the subscription
await redisSubscriberService.SubscribeToContactValidatedEvent(otpValidatedHandler.HandleContactValidatedEvent);

// Add middleware
app.UseMiddleware<AppExceptionHandlerMiddleware>();
app.UseSerilogRequestLogging();
app.UseCustomCors();
app.UseHeaderPropagation();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();












