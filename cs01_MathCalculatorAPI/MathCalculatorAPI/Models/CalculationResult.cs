
namespace MathCalculatorAPI.Models
{
	/*
		> Guid:
			Globally Unique Identifier it's a built-in struct
			generate a Unique ID

		> DateTime:
			a built-in strcut grab the current time
	*/

	/*
		> Positional record:
			From it's signature it create auto:
			- Get, Init
			- Constractor(...)
			- Deconstruct() -> init in once line 
			- Operator overlading (==)
	*/
	public record CalculationResult(	//  positional record
		Guid	Id,
		double	OperandA,
		double	OperandB,
		string	Operation,
		double	Result,
		DateTime	CreatedAt
	);
}
