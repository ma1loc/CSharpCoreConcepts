/*	MVC rule:
	If it prints to the screen (Console.WriteLine) or reads from the keyboard
	(Console.ReadLine), it belongs exclusively inside the View.
*/

/*	naming namespace rule:
	Your namespace path must match your folder path exactly
*/
using	StudentGradeCalculator.Controllers;
using	StudentGradeCalculator.Models;

namespace	StudentGradeCalculator.Views
{
	// Main Class name most match the file name
	public class ConsoleView
	{

		private readonly GradeController _controller;

		public ConsoleView(GradeController controller)
		{
			_controller = controller;
		}

		public void DisplayMenu()
		{
			// app title
			Console.WriteLine("\n========================================");
			Console.WriteLine("   Student Grade Calculator v1.0");
			Console.WriteLine("========================================\n");

			// program options
			Console.WriteLine("[1] Add Student");
			Console.WriteLine("[2] Enter Grade");
			Console.WriteLine("[3] View Report Card");
			Console.WriteLine("[4] List All Students");
			Console.WriteLine("[5] List Passing Students");
			Console.WriteLine("[6] Undo Last Grade Entry");
			Console.WriteLine("[0] Exit");
		}

		public void AddStudentView()
		{
			Console.Write("\nEnter student name: ");
			string? studentName = Console.ReadLine();
			Console.Write("Enter student ID: ");
			string? studentId = Console.ReadLine();

			/*
				In C# there's an operator that check if the left is assigned with null
				if yes will assing it auto with the right value.
				if student Name or student Id is null set it with ("" -> empty value)
			*/
			_controller.AddStudent(studentName ?? "", studentId ?? "");

			Console.WriteLine("Student added successfully!\n");
		}

		public void EnterGradeView()
		{
			Console.Write("\nEnter student ID: ");
			string? studnetId = Console.ReadLine();

			Console.Write("Enter subject name: ");
			string? studentSubjectName = Console.ReadLine();

			Console.Write("Enter grade (0-20): ");
			string? studentGrade = Console.ReadLine();

			/*
				> In C# double is a strcut based, TryParse one of the double struct members
					that helps use to conver from string object into a double.
				> Syntax:
					TryParse take (input(string object), output of type out double result)
					out -> enforcement in the method to init the result.
				> Note: In C#, you can pass an uninitialized variable to a method only if you use the out modifier
			*/
			if (double.TryParse(studentGrade, out double resultGrade))
			{
				_controller.EnterGrade(studnetId ?? "", studentSubjectName ?? "", resultGrade);
				Console.WriteLine("Grade added!\n");
			}
			else
				Console.WriteLine("Invalid grade — please enter a number!\n");
		}

		public void ViewReportCard()
		{
			Console.Write("\nEnter student ID:");
			string? studentId = Console.ReadLine();

			try
			{
				Student? student = _controller.ViewReportCard(studentId ?? "");
				if (student is not null)
				{
					Console.WriteLine("========================================");
					// > String Interpolation: '$'
					Console.WriteLine($" REPORT CARD — {student.Name} ({studentId})");
					Console.WriteLine("========================================");
					foreach (var grade in student.Grades)
					{
						// > PadRight: gives right padding based on the output length.
						Console.WriteLine($"  {grade.Key.PadRight(15)} : {grade.Value:F2}");
					}
					Console.WriteLine("  ----------------------------");
					Console.WriteLine($"  {"Average".PadRight(15)} : {student.Average} / 20");
					Console.WriteLine($"  {"Status".PadRight(15)} : {((student.Average >= 10) ? "PASSED" : "FAILED")}");
				}
			}
			catch (ArgumentException ex)
			{
				Console.WriteLine($"[-] Error: {ex.Message}");
			}
		}

		public void	ListAllStudentsView()
		{
			int studentCounter = 0;
			foreach (var Student in _controller.ListAllStudents())
			{
				Console.WriteLine($"Student Name: {Student.Name.PadRight(15)} | It's ID: {Student.Id}");
				studentCounter++;
			}
			Console.WriteLine($"> Total Student: {studentCounter}");
		}

		public void ListPassingStudentsView()
		{
			int passingStudentCounter = 0;
			foreach (var Student in _controller.ListPassingStudents())
			{
				Console.WriteLine($"Student Name: {Student.Name.PadRight(15)} | It's ID: {Student.Id}");
				passingStudentCounter++;
			}
			Console.WriteLine($"> Total Passing Student: {passingStudentCounter}");
		}

		public void UndoLastGradeEntryView()
		{
			_controller.UndoLastGradeEntry();
			Console.WriteLine("Last grade entry undone!");
		}

		public async Task	RunEventLoop()
		{
			while (true)	// Event loop
			{
				DisplayMenu();
				Console.Write("\n> Choose an option: ");
				// .KeyChar -> in case you press a key like "shift" that has more then one char
				char input_opt = Console.ReadKey().KeyChar;
				Console.WriteLine();	// New-line

				switch (input_opt)
				{
					case '1':
						AddStudentView();
						break ;
					case '2':
						EnterGradeView();
						break ;
					case '3':
						ViewReportCard();
						break ;
					case '4':
						ListAllStudentsView();
						break ;
					case '5':
						ListPassingStudentsView();
						break ;
					case '6':
						UndoLastGradeEntryView();
						break ;
					case '0':
						/*
							Before existing we Serializer object into a JSON text file
								called students.json
						*/
						await _controller.SaveData("students.json");
						Console.WriteLine("Goodbay!!!");
						return ;
					default:
						break ;
				}
			}
		}

		
	}
}
