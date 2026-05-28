
using StudentGradeCalculator.Models;

namespace StudentGradeCalculator.Services
{
	public interface	IGradeService
	{
		void	AddStudent(Student student);
		void	AddGrade(string studentId, string subject, double score);
		void	UndoLastGrade();


		/*	> yield:
			make the return stream rather then return all the list at once
			it can be big list, most use the yield keyword with the IEnumerable
			return type with <Generic type>, and it's read-only 
		*/
		IEnumerable<Student> GetPassingStudents();
		IEnumerable<Student> GetAllStudents();

		/*
			> Task:
				A class that represents a job that may return now or later.
				High level thread management — when data is ready it returns it.
			
			> async:
				Allows the use of await inside the method.
			
			> await:
				Releases the current thread instead of blocking it.
				The job can be picked up by another free thread from the pool.
				When done, execution comes back to where it left off.
			
			> SaveAsync:
				- Need to know Json:
					> Json is plain-text fromat that used to data-interchange between end-points.

				- Need to know JsonSerialization:
					> JsonSerialization is a process of convertion from a complex data struct or
					an object into a Json(JavaScript Object notiation) to make the data stord easy
					int plan-text file and transmitted over a network to other end-point 

				- Serialization && Deserialization
					> Serialization: converting from an object to JSON or XML
					> Deserialization: is the opposite of Serialization, From JSON or XML to Object
		*/
		Task	SaveAsync(string filePath);

		Task	LoadAsync(string filePath);
	}
}
