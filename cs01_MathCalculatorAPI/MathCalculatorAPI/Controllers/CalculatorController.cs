
using Microsoft.AspNetCore.Mvc;

namespace MathCalculatorAPI.Controllers
{
	/*
		> ApiController:
			'ApiController' is build-in attribute
			
	*/
	[ApiController]

	/*
		> What is Controller:
			entry point for HTTP requests

		> ControllerBase:
			Is a build in class to get a pre-build metodes like:
			Ok(result)
			NotFound();
			... etc
			- this is a method that give a raw response header/body
				ready to use it to send an HTTP reponse.
				WITHOUT IT; will create it by yourself.
	*/
	public class CalculatorController : ControllerBase
	{
		
	}
}