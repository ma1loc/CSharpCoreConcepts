# 📚 Project Subject: Student Grade Calculator — Console Application

## Overview

Build a **C# console application** that manages students, subjects, and grades for a school. The system allows users to add students, assign grades per subject, calculate averages, and generate report cards — all from the terminal.

This project is designed as a **practice exercise** to cover a wide range of C# language features and keywords in a realistic, meaningful context.

---

## Objectives

By completing this project, you will practice and demonstrate understanding of:

- Object-oriented design patterns (MVC architecture)
- Dependency Injection
- C# language features: attributes, properties, records, structs, generics
- Collection types: `List<T>`, `Dictionary<TKey, TValue>`, `Queue<T>`
- Advanced keywords and expressions

---

## Application Features

### Core Features

1. **Add a new student** (name, ID, list of subjects)
2. **Enter grades** for a student per subject
3. **Calculate the average grade** for a student
4. **Display a report card** for a student
5. **List all students** with their averages
6. **Filter students** by grade status (Passed / Failed)
7. **Undo queue** — store and cancel the last grade entry

---

## Architecture: MVC Pattern

```
/StudentGradeCalculator
│
├── Models/
│   ├── Student.cs          → class, record, struct
│   ├── Grade.cs            → record, required, with
│   └── Subject.cs          → struct
│
├── Views/
│   └── ConsoleView.cs      → Display output to terminal
│
├── Controllers/
│   └── GradeController.cs  → Business logic, DI, async methods
│
├── Services/
│   ├── IGradeService.cs    → Interface
│   └── GradeService.cs     → Implementation with yield, IEnumerable
│
├── Attributes/
│   └── ValidGradeAttribute.cs  → Custom attribute
│
└── Program.cs              → Entry point, DI setup
```

---

## Features & Keywords Mapping

### 🔷 Features to Implement

| Feature | Where to Use It |
|---|---|
| **Dependency Injection** | Inject `IGradeService` into `GradeController` via constructor |
| **MVC Architecture** | Separate `Model`, `View`, `Controller` folders/classes |
| **Attributes** | Create a `[ValidGrade]` custom attribute on grade input methods |
| **get / set** | All model properties use `get; set;` or `get; init;` |
| **Deconstruct** | Deconstruct a `Student` into `(name, id, average)` |
| **List / Dictionary / Queue** | Store students in `List<Student>`, grades in `Dictionary<string, double>`, undo in `Queue<GradeEntry>` |
| **TODO** | Mark incomplete features with `// TODO:` comments |
| **struct** | Use `struct SubjectInfo` for lightweight subject data |
| **class** | `Student`, `GradeController`, `GradeService` |
| **record** | Use `record GradeEntry(string Subject, double Score)` for immutable grade entries |
| **IEnumerable** | `GradeService` returns `IEnumerable<Student>` for filtered lists |

---

### 🔶 Keywords to Use

| Keyword | Where to Use It |
|---|---|
| `var` | Local variable declarations throughout |
| `in` | Pass collections by reference with `in` parameter modifier |
| `out` | Parse grade input with `double.TryParse(..., out var grade)` |
| `object` | Generic display method accepting `object data` |
| `as` | Safe casting: `data as Student` |
| `is` | Pattern matching: `if (data is Student s)` |
| `with` | Copy a record with modifications: `entry with { Score = newScore }` |
| `where` | Generic constraint: `where T : IStudent` |
| `required` | Required property in a model: `required string Name { get; init; }` |
| `sealed` | `sealed class FinalReportCard` — prevent inheritance |
| **Lambda expression** | LINQ filtering: `.Where(s => s.Average >= 10)` |
| `ref` | Pass a grade total by reference for accumulation |
| `base` | Call base constructor in a derived class |
| `yield` | `yield return` in `IEnumerable<Student>` filtered method |
| `async / await` | Simulate async save/load of student data |
| `foreach` | Iterate over students, subjects, and grades |

---

## Models Detail

### `Student` class
```csharp
public class Student
{
    public required string Name { get; init; }
    public required string Id   { get; init; }
    public Dictionary<string, double> Grades { get; set; } = new();

    public double Average => Grades.Count == 0 ? 0 : Grades.Values.Sum() / Grades.Count;

    // Deconstruct
    public void Deconstruct(out string name, out string id, out double average)
    {
        name    = Name;
        id      = Id;
        average = Average;
    }
}
```

### `GradeEntry` record
```csharp
public record GradeEntry(string Subject, double Score);
// Usage: var updated = entry with { Score = 15.5 };
```

### `SubjectInfo` struct
```csharp
public struct SubjectInfo
{
    public string Name;
    public int    Coefficient;
}
```

---

## Service Layer

### `IGradeService` interface
```csharp
public interface IGradeService
{
    void   AddStudent(Student student);
    void   AddGrade(string studentId, string subject, double score);
    IEnumerable<Student> GetPassingStudents();   // uses yield
    IEnumerable<Student> GetAllStudents();
    Task   SaveAsync(string filePath);           // async / await
}
```

---

## Custom Attribute

```csharp
[AttributeUsage(AttributeTargets.Method)]
public class ValidGradeAttribute : Attribute
{
    public double Min { get; }
    public double Max { get; }

    public ValidGradeAttribute(double min = 0, double max = 20)
    {
        Min = min;
        Max = max;
    }
}

// Usage on method:
[ValidGrade(0, 20)]
public void AddGrade(string studentId, string subject, double score) { ... }
```

---

## Sample Console Interaction

```
========================================
   Student Grade Calculator v1.0
========================================

[1] Add Student
[2] Enter Grade
[3] View Report Card
[4] List All Students
[5] List Passing Students
[6] Undo Last Grade Entry
[0] Exit

> Choose an option: 1

Enter student name: Ahmed Benali
Enter student ID : S001
Student added successfully!

> Choose an option: 2

Enter student ID    : S001
Enter subject name  : Mathematics
Enter grade (0-20)  : 17.5
Grade added!

> Choose an option: 3

========================================
  REPORT CARD — Ahmed Benali (S001)
========================================
  Mathematics    : 17.50
  Physics        : 14.00
  English        : 16.00
  ----------------------------
  Average        : 15.83 / 20
  Status         : ✅ PASSED
========================================
```

---

## Grading Criteria (for instructor)

| Criterion | Points |
|---|---|
| MVC structure respected | 20 pts |
| All C# features correctly used | 30 pts |
| All required keywords present | 20 pts |
| Application runs without errors | 15 pts |
| Code readability & comments | 15 pts |
| **Total** | **100 pts** |

---

## Deliverables

- [ ] Full C# console project (`.sln` + `.csproj`)
- [ ] All classes in correct MVC folders
- [ ] `README.md` explaining how to run the app
- [ ] Each required feature/keyword commented with `// FEATURE: ...` or `// KEYWORD: ...`

---

## Tips

- Start with the **Models** — define `Student`, `GradeEntry`, and `SubjectInfo` first.
- Then build the **Service layer** with a simple in-memory list.
- Wire the **Controller** with DI last, using `Program.cs` as the composition root.
- Use `// TODO:` as placeholders while you build step by step.
- Test each feature in isolation before connecting them.

---

*Good luck! Focus on understanding **why** each feature is used, not just placing it somewhere.* 🎓