# 📚 Student Grade Calculator

A C# console application for managing students, subjects, and grades.
Built as a practice project to cover core C# language features and MVC architecture.

---

## 🚀 How to Run

```bash
# clone the project
git clone https://github.com/ma1loc/C_Sharp_core-concepts.git
cd cs00_StudentGradeCalculator

# run the app
dotnet run --project StudentGradeCalculator
```

> Requires **.NET 10.0** or later.

---

## 📋 Features

| Option | Description |
|---|---|
| `[1]` Add Student | Add a new student with name and ID |
| `[2]` Enter Grade | Add a grade for a student by subject |
| `[3]` View Report Card | Display a student's full report card |
| `[4]` List All Students | Show all students with their IDs |
| `[5]` List Passing Students | Show only students with average ≥ 10 |
| `[6]` Undo Last Grade | Remove the last grade entry |
| `[0]` Exit | Save data and exit the app |

---

## 🗂️ Project Structure

```
StudentGradeCalculator/
│
├── Models/
│   ├── Student.cs           → class, required, init, Deconstruct
│   ├── Grade.cs             → record, immutability, with expression
│   └── Subject.cs           → struct
│
├── Views/
│   └── ConsoleView.cs       → all Console input/output (MVC View)
│
├── Controllers/
│   └── GradeControllers.cs  → orchestrates View ↔ Service (MVC Controller)
│
├── Services/
│   ├── IGradeService.cs     → interface (service contract)
│   └── GradeService.cs      → business logic + in-memory database
│
├── Attributes/
│   └── ValidGradeAttribute.cs → custom attribute for grade validation
│
└── Program.cs               → entry point, DI wiring
```

---

## 🏗️ Architecture

This project follows the **MVC pattern:**

```
ConsoleView          → reads input, prints output ONLY
     ↓
GradeController      → receives input, calls service
     ↓
GradeService         → owns List<Student>, all business logic
     ↓
Models               → Student, GradeEntry, SubjectInfo
```

**Dependency Injection** is wired manually in `Program.cs`:

```csharp
var gradeService = new GradeService();
var controller   = new GradeController(gradeService);
var view         = new ConsoleView(controller);
```

---

## 💾 Data Persistence

Student data is saved automatically to `students.json` when you exit the app,
and loaded back when you restart it.

```
exit app  → SaveAsync()  → students.json
start app → LoadAsync()  → students.json → List<Student>
```

---

## 🔷 C# Features Practiced

| Feature | Where |
|---|---|
| `class` | Student, GradeController, GradeService |
| `record` | GradeEntry — immutable grade snapshot |
| `struct` | SubjectInfo — lightweight subject data |
| `interface` | IGradeService — service contract |
| `required` + `init` | Student.Name, Student.Id |
| `get` / `set` | All model properties |
| `Deconstruct` | Student → (name, id, average) |
| `IEnumerable` + `yield` | GetAllStudents, GetPassingStudents |
| `async` / `await` + `Task` | SaveAsync, LoadAsync |
| `List<T>` | Student database |
| `Dictionary<K,V>` | Student grades |
| `Stack<T>` | Undo history (LIFO) |
| Custom `Attribute` | ValidGradeAttribute on AddGrade |
| Dependency Injection | Constructor injection throughout |

---

## 🔶 C# Keywords Practiced

`var` `required` `readonly` `out` `foreach` `yield` `async` `await`
`is` `is not` `??` `?` (nullable) lambda expressions `string interpolation`

---

## 📦 Sample Session

```
========================================
   Student Grade Calculator v1.0
========================================

[1] Add Student
[2] Enter Grade
...

> Choose an option: 1
Enter student name: Ahmed Benali
Enter student ID: S001
Student added successfully!

> Choose an option: 2
Enter student ID: S001
Enter subject name: Mathematics
Enter grade (0-20): 17.5
Grade added!

> Choose an option: 3
Enter student ID: S001
========================================
 REPORT CARD — Ahmed Benali (S001)
========================================
  Mathematics     : 17.50
  ----------------------------
  Average         : 17.50 / 20
  Status          : PASSED
```

---

## 👨‍💻 Author

Built from scratch as a C# learning project — no copy paste, fully understood. 💪