using PotatoPackageManager.Base;
using PotatoPackageManager.Base.Parser;
using System;
using System.IO;

namespace PotatoPackageManager
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            dynamic data;

            try
            {
                //LanguageManager lang = new LanguageManager("en-US");

                using StreamReader reader = new StreamReader("ArgumentMap/Default.json");
                data = new ArgumentMapParser().Parse(reader.ReadToEnd());

                if (args.Length == 0 && data.defaultaction == null)
                    throw new ArgumentNullException("args");
                else if (args.Length == 0 && data.defaultaction != null)
                {
                    Console.WriteLine(data.defaultaction);
                }
                else
                {
                }
            }
            catch (ArgumentNullException ex)
            {
                ExceptionManager.Throw(ex, true);
            }

#if DEBUG
            Console.Write("デバッグ環境です。続行するには何かキーを押してください...");
            Console.ReadKey();
#endif
        }
    }
}