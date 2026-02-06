using System;
using System.Reflection;

class Task
{
    public  string title;
    public string details;
    public DateTime time;
    public bool completed;

    public Task(string title,string details,bool completed)
    {
        this.title = title;
        this.details = details;
        this.completed = completed;
        this.time = DateTime.Now;
    }
}
class User
{
    List<Task> ?tasks;
    public User(List<Task> tasks)
    {
        this.tasks = tasks;
    }

    private void Pause()
    {
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
        Console.Clear();
    }

    public void Show_Menu()
    {
        Console.WriteLine("========================");
        Console.WriteLine("Task Manager");
        Console.WriteLine("=========================");
        Console.WriteLine("1)Add Task");
        Console.WriteLine("2)View Tasks");
        Console.WriteLine("3)Delete Task");
        Console.WriteLine("4)Mark Complete");
        Console.WriteLine("5)Exit ");
        Console.Write("Enter your choice: ");
        int choice = int.Parse(Console.ReadLine() ?? "0");
        switch(choice)
        {
            case 1: Add_Task();break;
            case 2: View_Task();break;
            case 3: Delete_Task();break;
            case 4: Mark_Complete();break;
            case 5: Environment.Exit(0);break;
        }
    }
    private void Add_Task()
    {
        Console.Clear();
        Console.Write("Enter Task name: ");
        string name = Console.ReadLine() ?? "";
        Console.Write("Enter details about Task: ");
        string details = Console.ReadLine() ?? "";
        Task task = new Task(name, details, false);
        tasks.Add(task);
        Console.WriteLine("Task Added Successfully");
        Pause();
    }
    private void View_Task()
    {
        Console.Clear();
        Console.Write($"{"Task",-20}{"Details",-20},{"Time",-20}{"Status"}");
        foreach(var task in tasks)
        {
            Console.WriteLine($"{task.title,-20}{task.details,-20},{task.time},{(task.completed ? "Completed" : "Incomplete")}");
        }
        Pause();
    }
    private void Delete_Task()
    {
        Console.Clear();
        Console.Write("Enter Task name to delete");
        string name = Console.ReadLine() ?? "";
        Task ?task = tasks.FirstOrDefault(u => u.title == name);
        tasks.Remove(task);
        Console.WriteLine("Task Deleted Successfully!");
        Pause();

    }
    private void Mark_Complete()
    {
        Console.Clear();
        Console.Write("Enter the Task to mark complete");
        string name = Console.ReadLine() ?? "";
        Task? task = tasks.FirstOrDefault(u => u.title == name);
        task.completed = true;
        Console.WriteLine("Mark as Completed Successfully!");
        Pause();
    }

}

class Program
{
    static void Main()
    {
        List<Task> tasks=new List<Task>();
        User user = new User(tasks);
        user.Show_Menu();

    }
}