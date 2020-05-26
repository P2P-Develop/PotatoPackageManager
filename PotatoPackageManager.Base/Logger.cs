using System;

namespace PotatoPackageManager.Base
{
    /// <summary>
    /// Manage log output class.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Infoログをコマンドラインに出力します。
        /// </summary>
        /// <param name="log">ログ内容。</param>
        public static void Info(string log)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(LanguageManager.GetText("InfoNormal"));
            Console.ResetColor();
            Console.WriteLine(log);
        }

        /// <summary>
        /// Warningログをコマンドラインに出力します。
        /// </summary>
        /// <param name="log">ログ内容。</param>
        public static void Warn(string log)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(LanguageManager.GetText("WarningNormal"));
            Console.ResetColor();
            Console.WriteLine(log);
        }

        /// <summary>
        /// Errorログをコマンドラインに出力します。
        /// </summary>
        /// <param name="log">ログ内容。</param>
        public static void Error(string log)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(LanguageManager.GetText("ErrorNormal"));
            Console.ResetColor();
            Console.WriteLine(log);
        }
    }
}