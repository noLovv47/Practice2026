using System;
using System.IO;
using System.Linq;
using CommandLib;

namespace FileSystemCommands
{
    public class FindFilesCommand : ICommand
    {
        private readonly string _directoryPath;
        private readonly string _searchPattern;

        public FindFilesCommand(string directoryPath, string searchPattern)
        {
            if (string.IsNullOrEmpty(directoryPath))
                throw new ArgumentException("Путь к каталогу не может быть пустым", nameof(directoryPath));

            if (string.IsNullOrEmpty(searchPattern))
                throw new ArgumentException("Маска поиска не может быть пустой", nameof(searchPattern));

            _directoryPath = directoryPath;
            _searchPattern = searchPattern;
        }

        public void Execute()
        {
            if (!Directory.Exists(_directoryPath))
            {
                Console.WriteLine($"Каталог '{_directoryPath}' не найден.");
                return;
            }

            var files = Directory.GetFiles(_directoryPath, _searchPattern, SearchOption.AllDirectories);

            Console.WriteLine($"Найдено {files.Length} файлов по маске '{_searchPattern}':");
            foreach (var file in files)
            {
                Console.WriteLine($"  {file}");
            }
        }
    }
}