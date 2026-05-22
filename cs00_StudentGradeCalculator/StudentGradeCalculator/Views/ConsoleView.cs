/*	MVC rule:
	If it prints to the screen (Console.WriteLine) or reads from the keyboard
	(Console.ReadLine), it belongs exclusively inside the View.
*/

/*	naming namespace rule:
	Your namespace path must match your folder path exactly
*/
namespace	StudentGradeCalculator.Views
{
	// Main Class name most match the file name
	public class ConsoleView
	{
		// methode
		public void DisplayMenu()
		{
			// app title
			Console.WriteLine("========================================");
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

		public void	RunEventLoop()
		{
			DisplayMenu();

			// Event loop
			while (true)
			{
				Console.Write("\n> Choose an option: ");
				// .KeyChar -> in case you press a key like "shift" that has more then one char
				char input_opt = Console.ReadKey().KeyChar;
				Console.WriteLine();	// New-line

				switch (input_opt)
				{
					case '1':
						// TODO: adding new studnet
						
						break ;
					case '2':
						// TODO: enter grade based on the student ID
						break ;
					case '3':
						// TODO: view report card
						break ;
					case '4':
						// TODO: view all students in the list
						break ;
					case '5':
						// TODO: list passing studnets
						break ;
					case '6':
						// TODO: undo last grade entry
						break ;
					case '0':
						Console.WriteLine("Goodbay!!!");
						return ;
					default:
						break ;
				}
			}
		}

		
	}
}