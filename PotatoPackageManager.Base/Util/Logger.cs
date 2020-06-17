using System;

namespace PotatoPackageManager.Base.Util
{
    /// <summary>
    /// ログ出力を管理します。
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
            Console.Write(LanguageChanger.GetText("InfoNormal"));
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
            Console.Write(LanguageChanger.GetText("WarningNormal"));
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
            Console.Write(LanguageChanger.GetText("ErrorNormal"));
            Console.ResetColor();
            Console.WriteLine(log);
        }
    }
}
