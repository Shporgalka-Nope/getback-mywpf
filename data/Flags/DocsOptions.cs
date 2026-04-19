using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Verb("help-me", HelpText = "Показать инструкцию по работе. Использовать с флагом -p Номер страницы " +
    "\nНомера страниц:" +
    "\n1 - Примечание " +
    "\n2 - Получение ИС от 1С" +
    "\n3 - Получение БД " +
    "\n4 - Если БД сделана через нейронку " +   
    "\n5 - Заполнение данных " +
    "\n6 - Получение страниц WPF " +
    "\n7 - Удаление и чистка следов")]
internal class DocsOptions
{
    [Option('p', "page", Required = true, HelpText = "Номер страницы.")]
    public int Page { get; set; }
}

