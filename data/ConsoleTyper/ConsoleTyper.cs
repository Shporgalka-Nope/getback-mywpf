using Scriban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
internal static class ConsoleTyper
{
    public static void PrintSuccesful(string line)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"[!] {line}.");
        Console.ResetColor();
    }

    public static void PrintError(string line) 
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine($"[ERR] {line}.");
        Console.ResetColor();
    }

    public static void PrintProcess(string line)
    {
        Console.WriteLine($"[/] {line}...");
    }
}

