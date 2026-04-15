using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace getback_mywpf.data.Helpers
{
    public class DbHelper : Helper
    {
        private string _connectionString;
        /// <summary>
        /// Initializes a new instance of the DbHelper class using the specified working directory, server host name,
        /// and database name.
        /// </summary>
        /// <param name="workingDir">The path to the working directory used by the helper.</param>
        /// <param name="hostname">The name or network address of the SQL Server instance to connect to.</param>
        /// <param name="database">The name of the database to connect to.</param>
        public DbHelper(string workingDir, string hostname, string database) : base(workingDir) 
        {
            _connectionString = $"Server={hostname};Database={database};Trusted_Connection=True;TrustServerCertificate=True;";
        }

        /// <summary>
        /// Perform the dotnet ef dbcontext scaffold command to generate the DbContext and entity classes based on the connection string provided.
        /// </summary>
        /// <returns>Returns true if scaffold was succesful. Otherwise false</returns>
        public bool Scaffold()
        {
            ConsoleTyper.PrintProcess("Попытка скаффолдинга");
            string command = $"ef dbcontext scaffold \"{_connectionString}\" Microsoft.EntityFrameworkCore.SqlServer -o Models --force --no-pluralize";
            if (!Execute(command))
            {
                ConsoleTyper.PrintError("Скаффолдинг провалился");
                PrintLogs();
                return false;
            }

            ConsoleTyper.PrintSuccesful("Скаффолдинг успешен");
            return true;
        }

        /// <summary>
        /// Apply migrations to the database using EmptyDbContext.
        /// </summary>
        /// <returns>Returns true if migrations were applied successfully. Otherwise false.</returns>
        public bool ApplyMigrations()
        {
            ConsoleTyper.PrintProcess("Применение миграций");
            try
            {
                using (EmptyDbContext context = new(_connectionString))
                {
                     context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                ConsoleTyper.PrintError("Применение миграций провалилось");
                PrintLogs();
                return false;
            }

            ConsoleTyper.PrintSuccesful("Применение миграций прошло успешно");
            return true;
        }

        public bool UpdateDB()
        {
            ConsoleTyper.PrintProcess("Создание миграций");
            string command = $"ef migrations add AutoMigration_{Guid.NewGuid()} --project {Directory.GetCurrentDirectory()} --startup-project {Directory.GetCurrentDirectory()}";
            if (!Execute(command))
            {
                ConsoleTyper.PrintError("Создание миграций провалилось");
                PrintLogs();
                return false;
            }
            ConsoleTyper.PrintSuccesful("Создание миграций прошло успешно");

            ConsoleTyper.PrintProcess("Удаление старой БД");
            command = $"ef database drop --force";
            if (!Execute(command))
            {
                ConsoleTyper.PrintError("Удаление старой БД провалилось");
                PrintLogs();
                return false;
            }
            ConsoleTyper.PrintSuccesful("База данных удалена успешно");

            ConsoleTyper.PrintProcess("Создание БД и применение миграций");
            command = $"ef database update --project {Directory.GetCurrentDirectory()} --startup-project {Directory.GetCurrentDirectory()}";
            if (!Execute(command))
            {
                ConsoleTyper.PrintError("Создание или применение миграций провалилось");
                PrintLogs();
                return false;
            }

            ConsoleTyper.PrintSuccesful("Создание и применение миграций прошло успешно");
            return Scaffold();
        }
    }
}
