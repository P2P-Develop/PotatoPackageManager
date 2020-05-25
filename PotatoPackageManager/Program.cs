using PotatoPackageManager.Base;
using System;
using static PotatoPackageManager.Base.LanguageManager;

namespace PotatoPackageManager
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                LanguageManager lang = new LanguageManager("en-US");

                if (args.Length == 0)
                {
                    throw new ArgumentNullException();
                }
            }
            catch (ArgumentNullException)
            {
                Logger.Log(GetText("NullArgumentError") + " " + GetText("SuggestionHelp"), LogType.Error);
            }

#if DEBUG
            Console.Write("デバッグ環境です。続行するには何かキーを押してください...");
            Console.ReadKey();
#endif
        }
    }
}
