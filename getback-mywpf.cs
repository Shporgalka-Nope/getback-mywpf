using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using CommandLine;
using Scriban;
using Scriban.Syntax;


string baseDir = AppDomain.CurrentDomain.BaseDirectory;
string templatePath = Path.Combine(baseDir, "templates");

Parser.Default.ParseArguments<Flags, OneCOptions, DbOptions>(args)
    .WithParsed<OneCOptions>(args => RunOneC()) //Run this method for get-1c
    .WithParsed<DbOptions>(args => RunDB()) //Run this method for get-db
    .WithParsed<Flags>(RunOptions) //Run this method for custom flags
    .WithNotParsed(RunError); //Run this method for --help and --version

//1C Methods
void RunOneC()
{
    Console.WriteLine("[/] Выгрузка ИС для 1С...");
    try
    {
        //Copy 1C database in current folder here
    }
    catch(Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine($"[ERR] Произошла ошибка при копировании: \n{ex.Message}");
        Console.ResetColor();
        return;
    }
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine($"[!] ИС выгружена в: {Directory.GetCurrentDirectory()}");
    Console.ResetColor();

}

void RunDB()
{
    //DB logic here
}
void RunOptions(Flags flags)
{
    Console.WriteLine("[/] Поиск файла .csproj...");
    string projFileDir;
    projFileDir = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.csproj").First();
    string projName = Path.GetFileNameWithoutExtension(projFileDir);
    Console.WriteLine($"[!] .csproj Найден: {projName}");
    Console.WriteLine($"[/] Скаффолдинг {projName}...");
    Directory.CreateDirectory("./Windows/");

    //Copy Windows
    string[] templates = Directory.GetFiles(templatePath);
    foreach(string template in templates)
    {
        
        string fileText = File.ReadAllText(Path.Combine(template));
        var rawSBN = Template.Parse(fileText);
        if (rawSBN.HasErrors) 
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"[ERR] Пропуск: {template}");
            Console.ResetColor();
            continue;
        }

        string readoutDir = $"./Windows/{Path.GetFileName(template)}";
        readoutDir = readoutDir.Replace(".sbn", "");

        var result = rawSBN.Render(new
        {
            csproj = projName,
            dbcontext = flags.ContextName,
            usermodelname = flags.UsersTableName,
            classname = Path.GetFileName(readoutDir.Replace(".xaml.cs", ""))
        });

        File.WriteAllText(readoutDir, result);
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"[!] Скопированно: {readoutDir}");
        Console.ResetColor();
    }
}

void RunError(IEnumerable<Error> errors)
{
    //Leave empty
}