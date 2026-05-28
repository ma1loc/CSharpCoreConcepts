
namespace StudentGradeCalculator.Attributes
{
	/*	Attribute

		> Attribute is a C# feature it's adding a tag to a method for example
			that gives extra infos about the methode.
		> Create a custom attribute most inharet from Attribute Class and
			Using a build-in attribute [AttributeUsage(AttributeTargets.Method)]
		
	*/

	[AttributeUsage(AttributeTargets.Method)]
	public class ValidGradeAttribute : Attribute
	{
		public double Min { get; }
		public double Max { get; }

		public ValidGradeAttribute(double min = 0, double max = 20)
		{
			Min = min;
			Max = max;
		}
	}
}
