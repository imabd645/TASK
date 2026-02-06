using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

class Task
{
    public string Title { get; set; }
    public string Details { get; set; }
    public DateTime Time { get; set; }
    public bool Completed { get; set; }

    public Task(string title, string details)
    {
        Title = title;
        Details = details;
        Time = DateTime.Now;
        Completed = false;
    }

    public Task() { }
}

class User
{
    private List<Task> tasks;
    private const string FilePath = "tasks.json";

    public User()
    {
        tasks = new List<Task>();
        LoadTasks();
    }

    private void Pause()
    {
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
        Console.Clear();
    }

    private void SaveTasks()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(FilePath, JsonSerializer.Serialize(tasks, options));
    }

    private void LoadTasks()
    {
        if (!File.Exists(FilePath)) return;
        tasks = JsonSerializer.Deserialize<List<Task>>(File.ReadAllText(FilePath)) ?? new List<Task>();
    }

    public void Show_Menu()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("========================");
            Console.WriteLine("Task Manager");
            Console.WriteLine("========================");
            Console.WriteLine("1) Add Task");
            Console.WriteLine("2) View Tasks");
            Console.WriteLine("3) Delete Task");
            Console.WriteLine("4) Mark Complete");
            Console.WriteLine("5) Exit");
            Console.Write("Enter your choice: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Pause();
                continue;
            }

            switch (choice)
            {
                case 1: Add_Task(); break;
                case 2: View_Task(); break;
                case 3: Delete_Task(); break;
                case 4: Mark_Complete(); break;
                case 5: running = false; break;
            }
        }
    }

    private void Add_Task()
    {
        Console.Clear();
        Console.Write("Enter task title: ");
        string title = Console.ReadLine() ?? "";
        Console.Write("Enter task details: ");
        string details = Console.ReadLine() ?? "";
        tasks.Add(new Task(title, details));
        SaveTasks();
        Pause();
    }

    private void View_Task()
    {
        Console.Clear();
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks found.");
            Pause();
            return;
        }

        for (int i = 0; i < tasks.Count; i++)
        {
            var t = tasks[i];
            Console.WriteLine($"{i + 1}) {t.Title} | {t.Details} | {t.Time} | {(t.Completed ? "Completed" : "Incomplete")}");
        }
        Pause();
    }

    private void Delete_Task()
    {
        Console.Clear();
        View_Task();
        Console.Write("Enter task number to delete: ");

        if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > tasks.Count)
        {
            Pause();
            return;
        }

        tasks.RemoveAt(index - 1);
        SaveTasks();
        Pause();
    }

    private void Mark_Complete()
    {
        Console.Clear();
        View_Task();
        Console.Write("Enter task number to mark complete: ");

        if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > tasks.Count)
        {
            Pause();
            return;
        }

        tasks[index - 1].Completed = true;
        SaveTasks();
        Pause();
    }
}

class Program
{
    static void Main()
    {
        new User().Show_Menu();
    }
}
