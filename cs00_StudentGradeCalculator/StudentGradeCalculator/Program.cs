using StudentGradeCalculator.Views;
using StudentGradeCalculator.Services;
using StudentGradeCalculator.Controllers;

namespace StudentGradeCalculator
{
    class Program
    {
		static async Task Main()
        {
            var gradeService = new GradeService();
            var controller = new GradeController(gradeService);
            var view = new ConsoleView(controller);

			await controller.LoadData("students.json");
            await view.RunEventLoop();
        }
    }
}