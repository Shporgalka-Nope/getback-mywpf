using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using CommandLine;
using data.Helpers;
using getback_mywpf.data.Helpers;
using Scriban;
using Scriban.Syntax;


string baseDir = AppDomain.CurrentDomain.BaseDirectory;
string templatePath = Path.Combine(baseDir, "templates");

Parser.Default.ParseArguments<Flags, OneCOptions, DbOptions>(args)
    .WithParsed<OneCOptions>(args => RunOneC())     //Run this method for get-1c
    .WithParsed<DbOptions>(args => RunDB(args))     //Run this method for get-db
    .WithParsed<Flags>(RunOptions)                  //Run this method for custom flags
    .WithNotParsed(RunError);                       //Run this method for --help and --version

//1C Methods
void RunOneC()
{
    Console.WriteLine("[/] Выгрузка ИС для 1С...");
    try
    {
        string dbSourcePath = Path.Combine(baseDir, "foreign-files", "ДемоОВ.dt");
        string dbDestPath = Path.Combine(Directory.GetCurrentDirectory(), "ДемоОВ.dt");
        File.Copy(dbSourcePath, dbDestPath);
    }
    catch(Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine($"[ERR] Произошла ошибка при выгрузке: \n{ex.Message}");
        Console.ResetColor();
        return;
    }
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine($"[!] ИС выгружена в: {Directory.GetCurrentDirectory()}");
    Console.ResetColor();

}

//DB Methods
void RunDB(DbOptions args)
{
    if(!args.SkipEfCore)
    {
        EfHelper efHelper = new(baseDir);
        //Check if dotnet ef is installed
        if(!efHelper.CheckEF()) return;
    }

    //Apply migrations
    DbHelper dbHelper = new(Directory.GetCurrentDirectory(), args.Server, args.Database);
    if(!args.SkipMigrations) if(!dbHelper.ApplyMigrations()) return;
    if(!dbHelper.Scaffold()) return;
}
void RunOptions(Flags flags)
{
    //Console.WriteLine("[/] Поиск файла .csproj...");
    //string projFileDir;
    //projFileDir = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.csproj").First();
    //string projName = Path.GetFileNameWithoutExtension(projFileDir);
    //Console.WriteLine($"[!] .csproj Найден: {projName}");

    //Console.WriteLine($"[/] Скаффолдинг {projName}...");
    //Directory.CreateDirectory("./Windows/");

    ////Copy Windows
    //string[] templates = Directory.GetFiles(templatePath);
    //foreach(string template in templates)
    //{
        
    //    string fileText = File.ReadAllText(Path.Combine(template));
    //    var rawSBN = Template.Parse(fileText);
    //    if (rawSBN.HasErrors) 
    //    {
    //        Console.ForegroundColor = ConsoleColor.DarkRed;
    //        Console.WriteLine($"[ERR] Пропуск: {template}");
    //        Console.ResetColor();
    //        continue;
    //    }

    //    string readoutDir = $"./Windows/{Path.GetFileName(template)}";
    //    readoutDir = readoutDir.Replace(".sbn", "");

    //    var result = rawSBN.Render(new
    //    {
    //        csproj = projName,
    //        dbcontext = flags.ContextName,
    //        usermodelname = flags.UsersTableName,
    //        classname = Path.GetFileName(readoutDir.Replace(".xaml.cs", "")),
    //        //manufacturer = flags.ManufacturesTableName,
    //        //supplier = flags.SuppliersTableName
    //    });

    //    File.WriteAllText(readoutDir, result);
    //    Console.ForegroundColor = ConsoleColor.DarkGreen;
    //    Console.WriteLine($"[!] Скопированно: {readoutDir}");
    //    Console.ResetColor();
    //}
}

void RunError(IEnumerable<Error> errors)
{
    //Leave empty
}