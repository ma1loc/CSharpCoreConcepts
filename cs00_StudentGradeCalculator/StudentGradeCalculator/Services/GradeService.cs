
using StudentGradeCalculator.Attributes;
using StudentGradeCalculator.Models;
using System.Text.Json;

namespace	StudentGradeCalculator.Services
{
	public	class GradeService : IGradeService
	{
		//	Databases = private fields
		// List with generic type of Student
		private readonly List<Student> _students = new();

		/*
			Stack<GradeEntry> stores snapshots of added grades
		    	used by the Undo feature [6] to remove the last
				grade entry.
		*/
		private readonly Stack<GradeEntry> _undoStack = new();

		public void AddStudent(Student student)
		{
			_students.Add(student);
		}

		public void UndoLastGrade()
		{
			if (_undoStack.Count == 0)
				return ;

			var lastGrade = _undoStack.Pop();
			foreach (var student in _students)
			{
				if (student.Id == lastGrade.studentId)
				{
					student.Grades.Remove(lastGrade.subject);	// remove besed on the key
					break ;
				}
			}
		}

		[ValidGrade(0, 20)]
		public void AddGrade(string studentId, string subject, double score)
		{
			/*
				find the student based on it's ID
				adding the Subject, score
			*/
			// var student = _students.FirstOrDefault(s => s.Id == studentId);
			// for (int i = 0; i < _students.Count; i++)	// old way
			
			Student? student = null;
			foreach (var s in _students)
			{
				// 's' is a placeholder for every student element
				if (s.Id == studentId)
				{
					student = s;
					break ;
				}
			}

			if (student == null)
				return ;
			
			// add new grade to the student
			student.Grades[subject] = score;

			/*
				save the Grade intry in the queue(LIFO)
				.Push -> build-in method adding item to the Stack that all
			*/
			_undoStack.Push(new GradeEntry(studentId, subject, score));
		}

		public IEnumerable<Student> GetPassingStudents()
		{
			foreach (var student in _students)
			{
				if (student.Average >= 10)
					yield return student;
			}
		}

		public IEnumerable<Student> GetAllStudents()
		{
			foreach (var student in _students)
			{
				yield return student;
			}
		}

		/*
			NOTE:
				> In C# just by running the program, C# creates a pool of threads ready to use
					just by calling async and await you are assigning a task to a free thread
			> Using Task at the signature of the method is not a return type like void
				but it's telling the method that is calling SaveAsync to WAIT
				until the job is done before continuing.
		*/
		public async Task   SaveAsync(string filePath)
		{
			// Serialization steps:

			/*	JsonSerializer.Serialize(_students) 

				> reads _students object and Convert from object to plain-text format
					and return it as string format.
			*/
			var text = JsonSerializer.Serialize(_students);


			/*	await File.WriteAllTextAsync(filePath, text)

				> await: wait intel the operation finish
				> File: Class From System.IO namespace
				> WriteAllTextAsync: build-in method write text to file
			*/
			await File.WriteAllTextAsync(filePath, text);
		}

		// TODO: KNOW THAT SHIT FOR WHAT ????
		public async Task LoadAsync(string filePath)
		{
			// checking the file if it's exist
			if (!File.Exists(filePath))
				return;

			/*
				> ReadAllTextAsync reading the JSON text file
					return a string format used in Deserialize.
			*/
			var text = await File.ReadAllTextAsync(filePath);

			/*
				> Deserialize<List<Student>>:
					converting from plain-text JSON string back to
					a List<Student> object that C# can work with.
				> AddRange():
					adding a mutiple student at once rather then
					looping into single sutdent and add it using Add()
			*/
			var students = JsonSerializer.Deserialize<List<Student>>(text);
			if (students != null)
				_students.AddRange(students);
		}
	}
}
