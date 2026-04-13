using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using CommandLine;
using Scriban;


string baseDir = AppDomain.CurrentDomain.BaseDirectory;
string templatePath = Path.Combine(baseDir, "templates");

Parser.Default.ParseArguments<Flags>(args)
    .WithParsed(RunOptions) //Run this method for custom flags
    .WithNotParsed(RunError); //Run this method for --help and --version

void RunOptions(Flags flags)
{
    Console.WriteLine("[/] Searching for .csproj file...");
    string projFileDir;
    projFileDir = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.csproj").First();
    string projName = Path.GetFileNameWithoutExtension(projFileDir);
    Console.WriteLine($"[!] Found .csproj: {projName}");
    Console.WriteLine($"[/] Scaffolding for {projName}...");
    Directory.CreateDirectory("./Windows/");

    //Copy AuthWindow
    string[] templates = Directory.GetFiles(templatePath);
    foreach(string template in templates)
    {
        
        string fileText = File.ReadAllText(Path.Combine(template));
        var rawSBN = Template.Parse(fileText);
        if (rawSBN.HasErrors) 
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"[ERR] Skipping: {template}");
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
            classname = Path.GetFileName(readoutDir.Replace(".xaml.cs", "")),
            manufacturer = flags.ManufacturesTableName,
            supplier = flags.SuppliersTableName
        });

        File.WriteAllText(readoutDir, result);
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"[!] Scaffolded: {readoutDir}");
        Console.ResetColor();
    }
}

void RunError(IEnumerable<Error> errors)
{
    //Leave empty
}


//Console.WriteLine("Hello World!");