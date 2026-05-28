
namespace	StudentGradeCalculator.Models
{
	/*
		this is the main class that will hold every student infos
	*/
	public	class Student
	{
		/*
			- syntax:
				> var start with '_' mean's it's a private member of a class.
			- fields:
				> most be privet in the class, use get,set to access them.
		*/

		// Fields == Private member of the class, _camelCase (With Underscore)
		private string _name = string.Empty;
		private string _id = string.Empty;
		public Dictionary<string, double> Grades { get; set; } = new();

		// Note: the statement of the Average is the same block the comments
		public double Average => Grades.Count == 0 ? 0 : Grades.Values.Sum() / Grades.Count;

		// public double Average
		// {
		// 	get
		// 	{
		// 		if (Grades.Count == 0)
		// 			return 0;
		// 		double Result = Grades.Values.Sum();
		// 		Result /= Grades.Count;
		// 		return Result;
		// 	}
		// }
		
		/*	Deconstruct is not destructor(finalizer)
			> Deconstruct:
				it's all about all the values in the Student class in one cline of code
				var(name, id, average) = student object
				Deconstruct will extract for you all the tree memeber in one ago
			> out:
				out(push-out) enforce init
		*/
	    public void Deconstruct(out string name, out string id, out double average)
		{
			name = this.Name;
			id = this.Id;
			average = this.Average;
		}

		public required string Name
		{
			get
			{
				return _name;
			}
			init
			{
				/*
					value keyword:
						is created auto by C# as placeholder of the "data"
							Student.Name = "data"
				*/
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentException("[-] Error: Student name cannot be empty or spaces.");
				_name = value;
			}
		}

		public required	string Id
		{
			get
			{
				return _id;
			}
			init
			{
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentException("[-] Error: Student ID cannot be empty or spaces.");
				_id = value;
			}
		}
	}
}
