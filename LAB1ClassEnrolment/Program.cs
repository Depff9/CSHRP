//Бряков Георгий
using System;
using System.Collections.Generic;

class Program
{
    static Dictionary<string, (int capacity, List<string> students)> courses = new();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Меню:\n1. Добавить курс\n2. Посмотреть курс\n3. Удалить курс\n4. Записать студента\n5. Показать студентов\n6. Удалить студента\n7. Выход\n");
            Console.Write("Выберите опцию: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddCourse(); break;
                case "2": ViewCourse(); break;
                case "3": DeleteCourse(); break;
                case "4": EnrollStudent(); break;
                case "5": ViewStudents(); break;
                case "6": RemoveStudent(); break;
                case "7": return;
                default: Console.WriteLine("Неверный выбор."); break;
            }
        }
    }

    static void AddCourse()
    {
        Console.Write("Название курса: ");
        string name = Console.ReadLine();
        Console.Write("Вместимость: ");
        int capacity = int.Parse(Console.ReadLine());
        courses[name] = (capacity, new List<string>());
        Console.WriteLine("Курс добавлен.");
    }

    static void ViewCourse()
    {
        Console.Write("Название курса: ");
        string name = Console.ReadLine();
        if (courses.TryGetValue(name, out var course))
            Console.WriteLine($"Курс: {name}, Вместимость: {course.capacity}, Студентов: {course.students.Count}");
        else
            Console.WriteLine("Курс не найден.");
    }

    static void DeleteCourse()
    {
        Console.Write("Название курса: ");
        string name = Console.ReadLine();
        if (courses.Remove(name))
            Console.WriteLine("Курс удален.");
        else
            Console.WriteLine("Курс не найден.");
    }

    static void EnrollStudent()
    {
        Console.Write("Название курса: ");
        string name = Console.ReadLine();
        Console.Write("Имя студента: ");
        string student = Console.ReadLine();
        if (courses.TryGetValue(name, out var course) && course.students.Count < course.capacity)
        {
            course.students.Add(student);
            Console.WriteLine("Студент записан.");
        }
        else
            Console.WriteLine("Курс не найден или нет мест.");
    }

    static void ViewStudents()
    {
        Console.Write("Название курса: ");
        string name = Console.ReadLine();
        if (courses.TryGetValue(name, out var course))
        {
            Console.WriteLine($"Студенты на курсе {name}:");
            foreach (var student in course.students)
                Console.WriteLine(student);
        }
        else
            Console.WriteLine("Курс не найден.");
    }

    static void RemoveStudent()
    {
        Console.Write("Название курса: ");
        string name = Console.ReadLine();
        Console.Write("Имя студента: ");
        string student = Console.ReadLine();
        if (courses.TryGetValue(name, out var course) && course.students.Remove(student))
            Console.WriteLine("Студент удален.");
        else
            Console.WriteLine("Студент не найден.");
    }
}
