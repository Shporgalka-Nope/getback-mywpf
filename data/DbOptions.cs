using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Verb("get-db", HelpText ="Выполнит серию команд для применения миграций и получения исходной БД." +
    "\nПолная последовательность: Скаффолд для получения DBContext -> Применение миграций -> Скаффолд для получения исходной БД.")]
public class DbOptions
{

}

