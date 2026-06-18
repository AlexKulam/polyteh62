using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static SemaphoreSlim semaphore = new SemaphoreSlim(3, 3);

    static async Task Student(int id)
    {
        Console.WriteLine($"Студент {id} ожидает компьютер");

        await semaphore.WaitAsync();

        Console.WriteLine($"Студент {id} сел за компьютер");

        await Task.Delay(3000);

        Console.WriteLine($"Студент {id} закончил работу");

        semaphore.Release();
    }

    static async Task Main()
    {
        List<Task> students = new List<Task>();

        for (int i = 1; i <= 10; i++)
        {
            students.Add(Student(i));
        }

        await Task.WhenAll(students);

        Console.WriteLine("Все студенты обслужены");
    }
}