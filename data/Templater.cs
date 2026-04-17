using Scriban;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Templater
{
    private readonly string BASE_DIR = AppDomain.CurrentDomain.BaseDirectory;
    private readonly string TEMPLATE_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "templates");

    private string _projName = null;
    private string _contextClassName = null;

    public Templater() {}
    public void Scaffold()
    {
        if (!FindProj()) return;
        ConsoleTyper.PrintProcess("Скаффолдинг .xaml и .xaml.cs");
        Directory.CreateDirectory("./Windows/");
        string[] templates = Directory.GetFiles(TEMPLATE_PATH);
        foreach (string template in templates)
        {

            string fileText = File.ReadAllText(template);
            var rawSBN = Template.Parse(fileText);
            if (rawSBN.HasErrors)
            {
                ConsoleTyper.PrintError($"Пропуск: Ошибка при парсинге шаблона {template}");
                continue;
            }

            string readoutDir = $"./Windows/{Path.GetFileName(template)}";
            readoutDir = readoutDir.Replace(".sbn", "");

            var result = rawSBN.Render(new
            {
                
            });

            File.WriteAllText(readoutDir, result);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"[!] Скопированно: {readoutDir}");
            Console.ResetColor();
        }

    }

    /// <summary>
    /// Looks for .csproj file in the current directory and sets _projName field.
    /// </summary>
    /// <returns>Returns true if found, false otherwise.</returns>
    private bool FindProj()
    {
        ConsoleTyper.PrintProcess("Поиск файла .csproj");
        string projFileDir = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.csproj").First();
        if(projFileDir != null)
        {
            string projName = Path.GetFileNameWithoutExtension(projFileDir);
            ConsoleTyper.PrintSuccesful($".csproj Найден: { projName}");
            _projName = projName;
            return true;
        }
        ConsoleTyper.PrintError("Файл .csproj не найден");
        return false;
    }
}

