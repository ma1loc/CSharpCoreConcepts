
using MathCalculatorAPI.Models;

namespace	MathCalculatorAPI.Services
{
	// > Business logic
	public interface ICalculatorService
	{
		/*
			> CalculateAsync:
				It's a generic type return of CalculationResult object
				using the 'Task' keyword mean's define the return types in c#
				Returns an asynchronous
			NOTE:
				At the end of the Method name is 'Async' just a Sign to
				Method is returning type of 'Task'
		*/
		Task<CalculationResult>	CalculateAsync(CalculationRequest Request);

		/*
			> GetHistoryAsync:
				It's return type is IEnumerable mean's it's read-only
		*/
		Task<IEnumerable<CalculationResult>> GetHistoryAsync();

		/*
			> GetByIdAsync:
				It's return time is CalculationResult object, but
				if the methode can't get any object by providing id
				will return 'null' becouse of '?' make the GetByIdAsync
				methode return 'null'
		*/
		Task<CalculationResult?> GetByIdAsync(Guid id);

		/*
			> ClearHistoryAsync:
				Job of this method is to clear the history has ben
				saved durring the program runing by calling the DELETE action
		*/
	    Task ClearHistoryAsync();
	}
}
