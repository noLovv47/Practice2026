using System;
using System.IO;
using System.Reflection;
using CommandLib;

namespace CommandRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Динамическая загрузка команд ===\n");

            string dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "FileSystemCommands", "bin", "Debug", "net10.0", "FileSystemCommands.dll");

            dllPath = Path.GetFullPath(dllPath);

            if (!File.Exists(dllPath))
            {
                Console.WriteLine($"Ошибка: не найдена библиотека '{dllPath}'");
                Console.WriteLine("Убедитесь, что проект FileSystemCommands собран.");
                return;
            }

            try
            {
                var assembly = Assembly.LoadFrom(dllPath);

                Console.WriteLine("Выберите команду:");
                Console.WriteLine("1. Подсчёт размера каталога");
                Console.WriteLine("2. Поиск файлов по маске");
                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Введите путь к каталогу: ");
                    string path = Console.ReadLine();
                    ExecuteCommand(assembly, "FileSystemCommands.DirectorySizeCommand", path);
                }
                else if (choice == "2")
                {
                    Console.Write("Введите путь к каталогу: ");
                    string path = Console.ReadLine();
                    Console.Write("Введите маску (например, *.txt): ");
                    string mask = Console.ReadLine();
                    ExecuteCommand(assembly, "FileSystemCommands.FindFilesCommand", path, mask);
                }
                else
                {
                    Console.WriteLine("Неверный выбор.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        static void ExecuteCommand(Assembly assembly, string typeName, params object[] args)
        {
            var type = assembly.GetType(typeName);
            if (type == null)
            {
                Console.WriteLine($"Тип '{typeName}' не найден.");
                return;
            }

            var constructor = type.GetConstructor(args.Select(a => a.GetType()).ToArray());
            if (constructor == null)
            {
                Console.WriteLine($"Конструктор с подходящими параметрами не найден.");
                return;
            }

            var instance = constructor.Invoke(args);
            var command = instance as ICommand;
            if (command == null)
            {
                Console.WriteLine($"Тип '{typeName}' не реализует ICommand.");
                return;
            }

            command.Execute();
        }
    }
}