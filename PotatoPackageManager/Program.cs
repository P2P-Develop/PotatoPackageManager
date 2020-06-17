using PotatoPackageManager.Base;
using PotatoPackageManager.Base.Exceptions;
using System;
using System.IO;
using static PotatoPackageManager.Base.Util.Logger;
using static System.Console;

namespace PotatoPackageManager
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                //LanguageManager lang = new LanguageManager("en-US");

                using StreamReader reader = new StreamReader("ArgumentMap/Default.json");
            }
            catch (ArgumentNullException ex)
            {
                ExceptionBuilder.Throw(ex, true);
            }
            catch (InvalidLanguageException ex)
            {
                Error(ex.Message);
                LanguageChanger.ChangeLanguage("en-US");
                Info("An internal code error has occurred and language has been switched to English.");
            }

#if DEBUG
            Write(@"デバッグ環境です。続行するには何かキーを押してください...");
            ReadKey();
#endif
        }
    }
}
