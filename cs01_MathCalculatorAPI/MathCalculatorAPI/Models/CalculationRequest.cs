
namespace MathCalculatorAPI.Models
{
	public class CalculationRequest
	{
		public double OperandA { get; set; }
		public double OperandB { get; set; }
		public string Operation { get; set; } = string.Empty;
	}
}
