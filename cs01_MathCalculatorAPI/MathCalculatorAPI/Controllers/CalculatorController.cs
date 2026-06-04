
using MathCalculatorAPI.Models;
using MathCalculatorAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MathCalculatorAPI.Controllers
{
	/*
		> ApiController:
			'ApiController' is built-in attribute
			it's a tag that tell ASP.NET Core framework
			to check the request entry-point if it's valid request format
	*/
	[ApiController]

	/*
		> Route("api/[controller]"):
			- Route:
				Is an attribute that defines the request path
				that will be handled by the controller.
				In other words, which request path is this controller interested in handling.
			- [controller]: => dynamic token replacement magic
				Is a placeholder inside the Route string
				ASP.NET Core reads the CLASS name automatically
				expects the CLASS name to end with 'Controller'
				extracts and removes 'Controller' from the CLASS name
				replaces [controller] with whatever is left
				in our case: CalculatorController -> Calculator
				if the CLASS name has no 'Controller' at the end
				will throw an error at runtime
			+ NOTE:
				- Class Name: CalculatorController
					Extracted Token: Calculator
					Base Route: api/Calculator -> take the Class name as it's
					- request URL matches:
						+ api/Calculator ➔ Matches
						+ api/calculator ➔ Matches
						+ api/CALCULATOR ➔ Matches
				- [controller] -> COMPILE-TIME placeholder ('done by the C# framwork')
	*/
	[Route("api/[controller]")]	// based path

	/*
		> What is 'controller' keyword:
			entry point for HTTP requests

		> ControllerBase:
			Is a built-in class to get a pre-build metodes like:
			Ok(result)
			NotFound();
			... etc
			- this is a method that give a raw response header/body
				ready to use it to send an HTTP reponse.
				WITHOUT IT; will create it by yourself.
		> NOTE:
			- controller behaves diff based on inharet from ControllerBase or Controller
				ControllerBase → REST pattern:
					✅ Model       → your data classes
					✅ Controller  → handles request, returns JSON
					❌ View        → doesn't exist

				Controller → MVC pattern:
					✅ Model       → your data classes
					✅ Controller  → handles request
					✅ View        → returns HTML
	*/
	public class CalculatorController : ControllerBase
	{
		// Using the Business logic interface
		private	readonly ICalculatorService _calculatorService;

		/*
			> NOTE:
				When a request commes from a client and match this controller
				ASP.NET Core will istantate an object from class based on the-
				constructor
		*/
		public CalculatorController(ICalculatorService calculatorService)
		{
			_calculatorService = calculatorService;
		}

		/*
			> HttpPost:
				Is a built-in attribute define two things:
					- Http action -> POST(method)
					- Resource path -> 'calculate'

		*/
		[HttpPost("calculate")]

		/*
			> IActionResult:
				Is an interface that represent an http response return type
					overall it's just a return type of http response.

			> [FromBody]:
				- Is an attribute that tells ASP.NET to read from the request:
					+ [FromBody] -> read from the body, becouse in our case we
						intrested in HttpPost verb, then we get JSON format
						that hold our body data.
					+ [FromQuery] -> read from URL Query
					+ [FromRoute] -> read form URL path

			> CalculationRequest:
				It's a class that act as a placeholder of body request.
				ASP.NET receive a request as Http raw req, then the ASP.NET
				check if Body format match the Content-type in the header:
					IF not match:
						return -> 415(Unsupported Media Type)
					ELSE:
						if the request commes like  JSON and XML
						the ASP.NET will parse the request by matching
						the keys the JSON or XML file with the members of the class
						it assinge the values to 'request' object
					+ BUT, what if the request missing some keys in the raw-request:
						becouse We set [Required] attribute to CalculationRequest
						if somting is missing well the ASP.NET will return a response
						of 400(Bad Request)
		*/
		public async Task<IActionResult> Calculate([FromBody] CalculationRequest request)
		{
			/*
				> 'await' _calculatorService.CalculateAsync(request):
					What await done here:
						TODO: KNOW
			*/
			try
			{
				var result = await _calculatorService.CalculateAsync(request);
				return Ok(result);
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/*
			> HttpGet:
				Is a built-in attribute define two things:
					- Http action -> GET(method)
					- Resource path -> 'history'
		*/
		[HttpGet("history")]
		public async Task<IActionResult> GetHistory()
		{
			var result = await _calculatorService.GetHistoryAsync();
			return Ok(result);
		}

		/*
			> HttpGet:
				Is a built-in attribute define two things:
					- Http action -> GET(method)
					- Resource path -> 'history/{id}'
			> {id}:
				It's a RUN-TIME placeholder ('Done by the client')
		*/
		[HttpGet("history/{id}")]
		public async Task<IActionResult?> GetById([FromRoute] Guid id)
		{
			var result = await _calculatorService.GetByIdAsync(id);
			if (result is null)
				return NotFound();
			return Ok(result);
		}

		[HttpDelete("history")]
		public async Task<IActionResult> ClearHistory()
		{
			await _calculatorService.ClearHistoryAsync();
			return NoContent();	// status-code of 204(No Content)
		}
	}
}

/*
	> NOTE:
		#1 - If there's no controller that match the request well the ASP.NET Core
			will drop an Http response of 404(Not Found) to the 'View'
		
		#2 - If match a controller but match no methode inside it 404(Not Found) returned
		
		#3 - If path exist but the verb/action is not, 405(Method not allowd)
*/
