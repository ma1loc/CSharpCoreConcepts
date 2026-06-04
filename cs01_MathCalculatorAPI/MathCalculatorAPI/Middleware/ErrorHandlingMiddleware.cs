
/*
	> Middleware What is it, What's for, How it works
	- What is it:
		+ Middleware is an HTTP request pipeline
	- What's for:
		+ Filter the requests comming from the pipeline
	- How it works:
		+ Request comming most respacte sequence of security
			ruls provided
	+ Summary:
		Middleware handle network traffic before request served
	
	> NOTE:
		- Middleware is not register by default, register in the program.cs
			with app.Use...(), and the order is matter
		- next() is a function that call the next Middleware in the chain
			based on your orderd and pass the HttpContyext object to the next
			Middleware.
		- Middleware 1, Middleware 2, Middleware 3 are just a generic naming
			it can be 10 or 100 of the Middlewares, it can be costume or built-in
		- Middleware read from the HttpContext object the request to extract from it:
			+ context.Request.Method
				- Http verb/action/method -> GET, POST, DELETE... etc
			+ context.Request.Path
				- Request URL -> Resurce path
			+ context.Request.Headers
				- Extract from the Header metadata like:
					user is Authorization(what you can do) to done that action
			+ context.Request.Body
				- User data format based on the Content-Type in the header
		- Middleware write in every chain to the request member of the HttpContext object
			+ If the request dosen't respect any Middleware ruls will write:
				context.Response.StatusCode = 500
			+ other think:
				context.Response.ContentType = "application/json"
				context.Response.WriteAsJsonAsync(...) -> body response
		-  Why does the request go back through middleware after the controller finishes
			As we know the request is going thghout a pipeline catched by the Middlewares
			after it's route to the correct controller then the business logic executed
*/

namespace MathCalculatorAPI.Middleware
{
	public class ErrorHandlingMiddleware
	{
		/*
			> RequestDelegate:
				Built-in ASP.NET Core, RequestDelegate is a function pointer to the next middleware
				Example;
					_next(context), calling next function that RequestDelegate pointed to and passing
						context object.
		*/
		private readonly RequestDelegate _next;

		/*
			TODO: Constractor for what ???
		*/
		public ErrorHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		/*
			> Invoke:
			
		*/

	}
}
