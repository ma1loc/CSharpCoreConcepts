
using System.ComponentModel.DataAnnotations;

namespace MathCalculatorAPI.Models
{
	public class CalculationRequest
	{
		/*
			> Required:
				It's a built-in attribute is used in propertys
				as enforcement value set managed by ASP.NET
			
			> NOTE: if a request ???
		*/
		[Required]

		/*
			> OperandA:
				It's not method becouse has no ()
				It's not a field becouse has {get; set;}
			It's property = hybrid between a field and a method.
		*/
		public double OperandA { get; set; }

		[Required]
		public double OperandB { get; set; }

		[Required]
		public string Operation { get; set; } = string.Empty;
	}
}
