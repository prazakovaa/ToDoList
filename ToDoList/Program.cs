using ToDoList.Data;
using ToDoList.Models;
using Microsoft.EntityFrameworkCore;
/*
AppDbContext context = new AppDbContext();

foreach (var s in context.Tasks.Include(s => s.Person).ToList())
{
    Console.WriteLine(s.Title);
    Console.WriteLine(s.Description);
    Console.WriteLine(s.Person?.Name);
    Console.WriteLine(s.IsCompleted);
}

try //pridava noveho, pomoci konzole - kazde spusteni vytvori novy zaznam
{
    context.Persons.Add(
        new Person
        {
            Name = "Alena Dvorakova",
            Id = 2
        }
        );
    context.SaveChanges();

    context.Tasks.Add(
        new TODOTask
        {
            Title = "Dishes",
            Description = "Put dishes in to the dishwasher",
            IsCompleted = false,
            PersonId = 2
        }
        );
    context.SaveChanges();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}


foreach (var people in context.Persons.Include(c => c.Tasks).ToList()) //vypise vsechny osoby a jejich ulohy    
{
    Console.WriteLine(people.Name);
    foreach (var st in people.Tasks)
    {
        Console.WriteLine("  " + st.Title);
    }
}


Person p = context.Persons //vypise ulohy konkretni osoby
    .FirstOrDefault(c => c.Name == "Jan Novak");
if (p != null)
{
    context.Entry(p).Collection(c => c.Tasks).Load();
}


var task = context.Tasks.FirstOrDefault(t => t.Id == 1); //najde specifickej ukol pro zmeny
if (task != null)
{
    task.IsCompleted = true;
    context.SaveChanges();
    Console.WriteLine($"Task '{task.Title}' is done.");
}
else
{
    Console.WriteLine("Task was not found.");
}
*/

static void Main()
{
    using var context = new AppDbContext();

    bool running = true;
    while (running)
    {
        Console.WriteLine("\nVyber akci:");
        Console.WriteLine("1 - Přidat osobu");
        Console.WriteLine("2 - Přidat úkol");
        Console.WriteLine("3 - Zobrazit osoby a jejich úkoly");
        Console.WriteLine("4 - Označit úkol jako splněný");
        Console.WriteLine("5 - Konec");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                AddPerson(context);
                break;
            case "2":
                AddTask(context);
                break;
            case "3":
                ShowAll(context);
                break;
            case "4":
                CompleteTask(context);
                break;
            case "5":
                running = false;
                break;
            default:
                Console.WriteLine("Neplatná volba.");
                break;
        }
    }
}

static void AddPerson(AppDbContext context)
{
    Console.Write("Zadej jméno nové osoby: ");
    string name = Console.ReadLine();

    var person = new Person { Name = name };
    context.Persons.Add(person);
    context.SaveChanges();

    Console.WriteLine($"Osoba '{name}' byla přidána.");
}

static void AddTask(AppDbContext context)
{
    Console.Write("Zadej název úkolu: ");
    string title = Console.ReadLine();

    Console.Write("Zadej popis úkolu (může být prázdný): ");
    string description = Console.ReadLine();

    Console.WriteLine("Vyber osobu podle Id:");
    foreach (var p in context.Persons.ToList())
    {
        Console.WriteLine($"{p.Id} - {p.Name}");
    }

    if (!int.TryParse(Console.ReadLine(), out int personId) ||
        context.Persons.FirstOrDefault(p => p.Id == personId) == null)
    {
        Console.WriteLine("Neplatné Id osoby.");
        return;
    }

    var task = new TODOTask
    {
        Title = title,
        Description = description,
        IsCompleted = false,
        PersonId = personId
    };

    context.Tasks.Add(task);
    context.SaveChanges();

    Console.WriteLine($"Úkol '{title}' byl přidán a přiřazen osobě Id={personId}.");
}

static void ShowAll(AppDbContext context)
{
    var persons = context.Persons
        .Include(p => p.Tasks)
        .ToList();

    foreach (var p in persons)
    {
        Console.WriteLine($"\n{p.Name} (Id={p.Id}):");
        foreach (var task in p.Tasks)
        {
            Console.WriteLine($"  {task.Id} - {task.Title} (Splněno: {task.IsCompleted})");
        }
    }
}

static void CompleteTask(AppDbContext context)
{
    Console.WriteLine("Vyber úkol podle Id, který chceš označit jako splněný:");
    foreach (var t in context.Tasks.Include(t => t.Person).ToList())
    {
        Console.WriteLine($"{t.Id} - {t.Title} (Osoba: {t.Person?.Name}, Splněno: {t.IsCompleted})");
    }

    if (!int.TryParse(Console.ReadLine(), out int taskId))
    {
        Console.WriteLine("Neplatné Id.");
        return;
    }

    var task = context.Tasks.FirstOrDefault(t => t.Id == taskId);
    if (task == null)
    {
        Console.WriteLine("Úkol nenalezen.");
        return;
    }

    task.IsCompleted = true;
    context.SaveChanges();

    Console.WriteLine($"Úkol '{task.Title}' byl označen jako splněný.");
}
