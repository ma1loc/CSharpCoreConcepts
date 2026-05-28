
using StudentGradeCalculator.Models;
using StudentGradeCalculator.Services;

namespace StudentGradeCalculator.Controllers
{
	public class GradeController
	{
		private	readonly IGradeService _gradeService;
		public GradeController(IGradeService gradeService)
		{
			_gradeService = gradeService;	
		}

		public async Task SaveData(string filePath)
		{
			await _gradeService.SaveAsync(filePath);
		}

		public async Task LoadData(string filePath)
		{
			await _gradeService.LoadAsync(filePath);
		}

		// Add Student feature
		public void AddStudent(string name, string id)
		{
			var student = new Student { Name = name, Id = id};
			_gradeService.AddStudent(student);
		}

		// Enter Grade feature
		public void EnterGrade(string id, string subjectName, double grade)
		{
			_gradeService.AddGrade(id, subjectName, grade);
		}

		// View Report feature
		// NOTE: '?' may return Student object OR null
		public Student? ViewReportCard(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
				throw new ArgumentException("Student ID cannot be empty or spaces.");

			foreach (var student in _gradeService.GetAllStudents())
			{
				if (student.Id == id)
					return student;
			}
			return null;
		}

		// List All Students feature
		public IEnumerable<Student> ListAllStudents()
		{
			return _gradeService.GetAllStudents();
		}

		// List Passing Students feature
		public IEnumerable<Student> ListPassingStudents()
		{
			return _gradeService.GetPassingStudents();
		}

		// Undo Last Grade feature
		public void UndoLastGradeEntry()
		{
			_gradeService.UndoLastGrade();
		}
	}
}
