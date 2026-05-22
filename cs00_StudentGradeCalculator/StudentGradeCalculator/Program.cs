/*
	System.Collections.Generic:

*/

using StudentGradeCalculator.Views;

namespace Program
{
	class Program
	{
		static void Main()  // Entry Point
		{
			var view = new ConsoleView();
			view.RunEventLoop();
		}
	}
}

