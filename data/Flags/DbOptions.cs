using CommandLine;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Verb("get-db", HelpText ="Выполнить серию команд для применения миграций и получения исходной БД.")]
public class DbOptions
{
    [Option('s', "server", Required = true, 
        HelpText ="Название сервера с базой данных. \nМожно узнать через ПКМ по БД -> Свойства -> Посмотреть свойства соединения -> Продукт/Имя сервера")]
    public string Server { get; set; }

    [Option('d', "database", Required = true,
        HelpText = "Название базы данных. \nМожно узнать через ПКМ по БД -> Свойства -> Посмотреть свойства соединения -> Соединение/База данных")]
    public string Database { get; set; }

    [Option('m', "skip-migrations", Required = false,
        HelpText = "Пропустить этап применения миграций.")]
    public bool SkipMigrations { get; set; } = false;

    [Option('e', "skip-ef-core", Required = false,
        HelpText = "Пропустить этап проверки наличия dotnet-ef")]
    public bool SkipEfCore { get; set; } = false;

    [Option('u', "update-database", Required = false,
        HelpText = "Создать миграции по таблицам в проекте. Удаляет старую БД ПОЛНОСТЬЮ, и создаёт новую с новыми таблицами.\nПрименять если были изменения в коде таблиц.")]
    public bool UpdateDatabase { get; set; } = false;
}

