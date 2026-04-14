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
    private readonly string MIGRATIONS_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "migrations");

    private string _projName = null;
    private string _contextClassName = null;

    public Templater()
    {
        FindProj();
    }
    public void ScaffoldMigrations()
    {
        ConsoleTyper.PrintProcess("Скаффолдинг миграций");
        Directory.CreateDirectory("./data/migrations");
        string[] templates = Directory.GetFiles(MIGRATIONS_PATH);

        foreach (string template in templates)
        {

            string fileText = File.ReadAllText(Path.Combine(template));
            var rawSBN = Template.Parse(fileText);
            if (rawSBN.HasErrors)
            {
                ConsoleTyper.PrintError($"Ошибка при парсинге шаблона: {template}");
                continue;
            }

            string readoutDir = $"./data/migrations/{Path.GetFileName(template)}";
            readoutDir = readoutDir.Replace(".sbn", "");

            var result = rawSBN.Render(new
            {
                csproj = _projName,
                //contextclassname = flags.ContextName,
                //usermodelname = flags.UsersTableName,
                //classname = Path.GetFileName(readoutDir.Replace(".xaml.cs", "")),
                //manufacturer = flags.ManufacturesTableName,
                //supplier = flags.SuppliersTableName
            });

            File.WriteAllText(readoutDir, result);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"[!] Скопированно: {readoutDir}");
            Console.ResetColor();
        }

    }

    private void FindProj()
    {
        ConsoleTyper.PrintProcess("Поиск файла .csproj");
        string projFileDir = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.csproj").First();
        if(projFileDir != null)
        {
            string projName = Path.GetFileNameWithoutExtension(projFileDir);
            ConsoleTyper.PrintSuccesful($".csproj Найден: { projName}");
            _projName = projName;
            return;
        }
        ConsoleTyper.PrintError("Файл .csproj не найден");
    }
}

