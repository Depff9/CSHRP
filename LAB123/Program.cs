// Георгий Бряков
using System;
using System.Collections.Generic;

enum Subject
{
    Математика,
    Физика,
    Химия,
    Информатика
}


struct Grade
{
    public Subject Subject { get; set; }
    public int Score { get; set; }
    public DateTime Date { get; set; }
}

class Program
{
    static Dictionary<string, (int capacity, List<(int id, string name, List<Grade> grades)> students)> courses = new();
    static int studentIdCounter = 1;

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Меню:\n1. Добавить курс\n2. Посмотреть курс\n3. Удалить курс\n" +
                "4. Записать студента\n5. Показать студентов\n6. Удалить студента\n" +
                "7. Добавить оценку\n8. Удалить оценку\n9. Показать оценки\n" +
                "10. Выход\n");
            Console.Write("Выберите опцию: ");
            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1": AddCourse(); break;
                    case "2": ViewCourse(); break;
                    case "3": DeleteCourse(); break;
                    case "4": EnrollStudent(); break;
                    case "5": ViewStudents(); break;
                    case "6": RemoveStudent(); break;
                    case "7": AddGrade(); break;
                    case "8": RemoveGrade(); break;
                    case "9": ViewGrades(); break;
                    case "10": return;
                    default: Console.WriteLine("Неверный выбор."); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }

    static void AddCourse()
    {
        Console.Write("Название курса: ");
        string name = Console.ReadLine();
        Console.Write("Вместимость: ");
        int capacity = int.Parse(Console.ReadLine());
        courses[name] = (capacity, new List<(int id, string name, List<Grade> grades)>());
        Console.WriteLine("Курс добавлен.");
    }

    static void ViewCourse()
    {
        Console.Write("Название курса: ");
        string name = Console.ReadLine();
        if (courses.TryGetValue(name, out var course))
            Console.WriteLine($"Курс: {name}, Вместимость: {course.capacity}, Студентов: {course.students.Count}");
        else
            throw new Exception("Курс не найден.");
    }

    static void DeleteCourse()
    {
        Console.Write("Название курса: ");
        string name = Console.ReadLine();
        if (courses.Remove(name))
            Console.WriteLine("Курс удален.");
        else
            throw new Exception("Курс не найден.");
    }

    static void EnrollStudent()
    {
        Console.Write("Название курса: ");
        string name = Console.ReadLine();
        Console.Write("Имя студента: ");
        string studentName = Console.ReadLine();
        if (courses.TryGetValue(name, out var course) && course.students.Count < course.capacity)
        {
            course.students.Add((studentIdCounter++, studentName, new List<Grade>()));
            Console.WriteLine("Студент записан.");
        }
        else
            throw new Exception("Курс не найден или нет мест.");
    }

    static void ViewStudents()
    {
        Console.Write("Название курса: ");
        string name = Console.ReadLine();
        if (courses.TryGetValue(name, out var course))
        {
            Console.WriteLine($"Студенты на курсе {name}:");
            foreach (var student in course.students)
                Console.WriteLine($"ID: {student.id}, Имя: {student.name}");
        }
        else
            throw new Exception("Курс не найден.");
    }

    static void RemoveStudent()
    {
        Console.Write("Название курса: ");
        string name = Console.ReadLine();
        Console.Write("ID студента: ");
        if (int.TryParse(Console.ReadLine(), out int studentId))
        {
            if (courses.TryGetValue(name, out var course))
            {
                var student = course.students.Find(s => s.id == studentId);
                if (student != default)
                {
                    course.students.Remove(student);
                    Console.WriteLine("Студент удален.");
                }
                else
                {
                    throw new Exception("Студент не найден.");
                }
            }
            else
            {
                throw new Exception("Курс не найден.");
            }
        }
        else
        {
            throw new Exception("Неверный ID студента.");
        }
    }

    static void AddGrade()
    {
        Console.Write("Название курса: ");
        string name = Console.ReadLine();
        Console.Write("ID студента: ");
        if (int.TryParse(Console.ReadLine(), out int studentId))
        {
            if (courses.TryGetValue(name, out var course))
            {
                var student = course.students.Find(s => s.id == studentId);
                if (student != default)
                {
                    Console.WriteLine("Выберите предмет:");
                    var subjects = Enum.GetValues(typeof(Subject));
                    for (int i = 0; i < subjects.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {subjects.GetValue(i)}");
                    }

                    Console.Write("Номер предмета: ");
                    if (int.TryParse(Console.ReadLine(), out int subjectIndex) && subjectIndex >= 1 && subjectIndex <= subjects.Length)
                    {
                        var subject = (Subject)subjects.GetValue(subjectIndex - 1);
                        Console.Write("Оценка: ");
                        if (int.TryParse(Console.ReadLine(), out int score))
                        {
                            Grade grade = new Grade
                            {
                                Subject = subject,
                                Score = score,
                                Date = DateTime.Now
                            };
                            student.grades.Add(grade);
                            Console.WriteLine("Оценка добавлена.");
                        }
                        else
                        {
                            Console.WriteLine("Неверная оценка.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверный номер предмета.");
                    }
                }
                else
                {
                    Console.WriteLine("Студент не найден.");
                }
            }
            else
            {
                Console.WriteLine("Курс не найден.");
            }
        }
        else
        {
            Console.WriteLine("Неверный ID студента.");
        }
    }

    static void RemoveGrade()
    {
        Console.Write("Название курса: ");
        string name = Console.ReadLine();
        Console.Write("ID студента: ");
        if (int.TryParse(Console.ReadLine(), out int studentId))
        {
            if (courses.TryGetValue(name, out var course))
            {
                var student = course.students.Find(s => s.id == studentId);
                if (student != default)
                {
                    Console.WriteLine("Выберите предмет:");
                    var subjects = Enum.GetValues(typeof(Subject));
                    for (int i = 0; i < subjects.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {subjects.GetValue(i)}");
                    }

                    Console.Write("Номер предмета: ");
                    if (int.TryParse(Console.ReadLine(), out int subjectIndex) && subjectIndex >= 1 && subjectIndex <= subjects.Length)
                    {
                        var subject = (Subject)subjects.GetValue(subjectIndex - 1);
                        var gradeExists = student.grades.Any(g => g.Subject == subject);
                        if (gradeExists)
                        {
                            var grade = student.grades.First(g => g.Subject == subject);
                            student.grades.Remove(grade);
                            Console.WriteLine("Оценка удалена.");
                        }
                        else
                        {
                            Console.WriteLine("Оценка не найдена.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверный номер предмета.");
                    }
                }
                else
                {
                    Console.WriteLine("Студент не найден.");
                }
            }
            else
            {
                Console.WriteLine("Курс не найден.");
            }
        }
        else
        {
            Console.WriteLine("Неверный ID студента.");
        }
    }

    static void ViewGrades()
    {
        Console.Write("Название курса: ");
        string name = Console.ReadLine();
        Console.Write("ID студента: ");
        if (int.TryParse(Console.ReadLine(), out int studentId))
        {
            if (courses.TryGetValue(name, out var course))
            {
                var student = course.students.Find(s => s.id == studentId);
                if (student != default)
                {
                    Console.WriteLine($"Оценки студента {student.name}:");
                    foreach (var grade in student.grades)
                    {
                        Console.WriteLine($"Предмет: {grade.Subject}, Оценка: {grade.Score}, Дата: {grade.Date}");
                    }
                }
                else
                {
                    Console.WriteLine("Студент не найден.");
                }
            }
            else
            {
                Console.WriteLine("Курс не найден.");
            }
        }
        else
        {
            Console.WriteLine("Неверный ID студента.");
        }
    }
}
