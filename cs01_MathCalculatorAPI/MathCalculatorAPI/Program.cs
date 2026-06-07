using MathCalculatorAPI.Middleware;
using MathCalculatorAPI.Services;

/*
	Entry point of the API logic:
	> WebApplication:
		- used to configure the HTTP pipeline, and routes.
	> CreateBuilder:
		- Create a WebApplicationBuilder object that will hold our config
*/
WebApplicationBuilder builder = WebApplication.CreateBuilder();

/*
	> Services.AddControllers();
		- Scan all my project files searching for Contollers based on the ControllerBase Inherite
			and ADD name of the find controller as:
				+ key: "CalculatorController"
				+ value: Instance of CalculatorController // in case req comming
*/
builder.Services.AddControllers();

/*
	> Services.AddScoped<>();
		- Adding Scope manually <Service(Interface), Implementation>
			It's like like:
				+ link interface to it's Implementation
			becosoe in the Controller we use Interface of the Service to make the Implementation
			not hardcoded and easy to pass the Implementation by calling the constractor
*/

/*
	> AddScoped:
		Using the 'AddScoped' Will make new instance of the CalculatorService
			object in every request comming.
	> AddSingleton:
		Oposite of the AddScoped will create an instance just once
			even it's a new request.
*/
// builder.Services.AddScoped<ICalculatorService, CalculatorService>();
builder.Services.AddSingleton<ICalculatorService, CalculatorService>();

// ----------------------------------- Swagger -----------------------------------
/*
	> AddEndpointsApiExplorer:

*/
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
// -------------------------------------------------------------------------------

WebApplication app = builder.Build();

// > NOTE: After the app Implementation is done it's time to setup middleware of it
app.UseMiddleware<ErrorHandlingMiddleware>();	// my Custom Middleware logic check
app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.MapControllers();

app.Run();
