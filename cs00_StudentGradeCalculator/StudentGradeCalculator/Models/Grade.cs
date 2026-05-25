
namespace StudentGradeCalculator.Models
{
	/*	record
		public record is by default a public record class
		futuar the record comming for to fix it overwriting
		it's done it auto for you '==' ...
		and it's Immutability by default
	*/
	public record GradeEntry(string Subject, double Score);
	// Usage: var updated = entry with { Score = 15.5 };
}
