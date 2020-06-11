using PotatoPackageManager.Base;
using PotatoPackageManager.Base.Exceptions;
using PotatoPackageManager.Base.Parser;
using System;
using System.IO;
using static PotatoPackageManager.Base.Logger;
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
                var data = ArgumentMapParser.Parse(reader.ReadToEnd());

                switch (args.Length)
                {
                    case 0 when data.defaultaction == null:
                        throw new ArgumentNullException(nameof(args));
                    case 0 when data.defaultaction != null:
                        WriteLine(data.defaultaction);
                        break;

                    default:
                        break;
                }
            }
            catch (ArgumentNullException ex)
            {
                ExceptionManager.Throw(ex, true);
            }
            catch (InvalidLanguageException ex)
            {
                Error(ex.Message);
                LanguageManager.ChangeLanguage("en-US");
                Info("An internal code error has occurred and language has been switched to English.");
            }

#if DEBUG
            Write(@"デバッグ環境です。続行するには何かキーを押してください...");
            ReadKey();
#endif
        }
    }
}
