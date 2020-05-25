using System;

namespace PotatoPackageManager.Base
{
    /// <summary>
    /// Manage log output class.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="type"></param>
        public static void Log(string log, LogType type)
        {
            switch (type)
            {
                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(LanguageManager.GetText("ErrorNormal"));
                    Console.ResetColor();
                    Console.WriteLine(log);
                    break;
                case LogType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(LanguageManager.GetText("WarningNormal"));
                    Console.ResetColor();
                    Console.WriteLine(log);
                    break;
                case LogType.Info:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(LanguageManager.GetText("InfoNormal"));
                    Console.ResetColor();
                    Console.WriteLine(log);
                    break;
            }
        }
    }
}
