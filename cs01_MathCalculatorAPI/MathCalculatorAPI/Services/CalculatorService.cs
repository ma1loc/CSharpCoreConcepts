
using MathCalculatorAPI.Services;
using MathCalculatorAPI.Models;

namespace	MathCalculatorAPI.Services
{
	public class CalculatorService : ICalculatorService
	{
		// NOTE: Private Field can not use { get; set; }
		private readonly List<CalculationResult> _history = new();

		public async Task<CalculationResult> CalculateAsync(CalculationRequest request)
		{
			/*
				> Enum.TryParse:
					Convert From a string operation into enum
					Generic type of OperationType enums i have
					will compare the enums key with the string
					if it's match will give me the value of the kay matches

					- request.Operation:
						string based value, ex: Add
					- ture:
						it's not case sensitive at all
						ex; Add == ADD
					- out var operation:
						placeholder for the value of the key matches
			*/
			if (Enum.TryParse<OperationType>(request.Operation, true, out var operation))
			{
				double operationResult = 0;
				switch (operation)
				{
					// NOTE: in enums there's no Deconstructor :(
					case OperationType.Add:
						operationResult = request.OperandA + request.OperandB;
						break;
					
					case OperationType.Subtract:
						operationResult = request.OperandA - request.OperandB;
						break;
					
					case OperationType.Multiply:
						operationResult = request.OperandA * request.OperandB;
						break;
					
					case OperationType.Divide:
						if (request.OperandB == 0)
							throw new ArgumentException("Undefined operation");
						operationResult = request.OperandA / request.OperandB;
						break;
				}

				// create a new Object hold our result
				var calculation = new CalculationResult
				(
					Guid.NewGuid(),	// Gen an ID
					request.OperandA,
					request.OperandB,
					request.Operation,
					operationResult,
					DateTime.UtcNow	// Time zone
				);

				_history.Add(calculation);

				/*
					what the fuck is the return syntax look like this ????
				*/
				return calculation;
			}
			else
				throw new ArgumentException("Invalid operation, bro.");
		}

		public async Task<IEnumerable<CalculationResult>> GetHistoryAsync()
		{
			return _history;
		}

		public async Task<CalculationResult?> GetByIdAsync(Guid id)
		{
			foreach (var calculation in _history)
			{
				if (calculation.Id == id)
					return calculation;
			}
			return null;
		}

		public async Task ClearHistoryAsync()
		{
			_history.Clear();
		}
	}
}
