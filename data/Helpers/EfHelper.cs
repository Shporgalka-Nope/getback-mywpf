using getback_mywpf.data.Helpers;
using Scriban;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.Helpers
{
    public class EfHelper : Helper
    {
        public EfHelper(string workingDir):base(workingDir) {}
        public bool CheckEF()
        {
            ConsoleTyper.PrintProcess("Проверка наличия dotnet ef");
            Execute("tool list --global");

            if(!LogOutput.Contains("dotnet-ef", StringComparison.OrdinalIgnoreCase))
            {
                ConsoleTyper.PrintError("dotnet ef не обнаружен");
                return RestoreEF();
            }
            else
            {
                ConsoleTyper.PrintSuccesful("dotnet ef найден");
                return true;
            }
        }

        private bool RestoreEF()
        {
            ConsoleTyper.PrintProcess("Установка dotnet ef");
            bool result = Execute("tool install --global dotnet-ef --version 8.0.0");

            if(!result)
            {
                ConsoleTyper.PrintError("Попытка установки dotnet ef провалилась");
                PrintLogs();
                return false;
            }
            ConsoleTyper.PrintSuccesful("dotnet ef успешно установлен");
            return true;
        }
    }
}
