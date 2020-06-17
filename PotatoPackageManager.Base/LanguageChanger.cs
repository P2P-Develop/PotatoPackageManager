using PotatoPackageManager.Base.Exceptions;
using System.Globalization;
using System.Threading;

namespace PotatoPackageManager.Base
{
    /// <summary>
    /// 多言語環境を管理します。
    /// </summary>
    public class LanguageChanger
    {
        /// <summary>
        /// デフォルトの言語を<see cref="CultureInfo"/>の再初期化により設定します。
        /// </summary>
        /// <param name="lang">デフォルトの言語コード。<br/>
        /// <example>例: ja-JP, en-US</example></param>
        public LanguageChanger(string lang)
        {
            try
            {
                Resource.Culture = new CultureInfo(lang);
            }
            catch (CultureNotFoundException)
            {
                throw new InvalidLanguageException(lang);
            }
            Thread.CurrentThread.CurrentCulture = Resource.Culture;
            Thread.CurrentThread.CurrentUICulture = Resource.Culture;
        }

        /// <summary>
        /// デフォルトの言語から新しい言語に<see cref="CultureInfo"/>を再初期化し設定します。
        /// </summary>
        /// <param name="afterlang">変更先言語コード。</param>
        public static void ChangeLanguage(string afterlang)
        {
            CultureInfo backup = Resource.Culture;
            Resource.Culture.ClearCachedData();
            Resource.Culture = null;
            try
            {
                Resource.Culture = new CultureInfo(afterlang);
            }
            catch (CultureNotFoundException)
            {
                Resource.Culture = backup;
                throw new InvalidLanguageException(afterlang);
            }
        }

        /// <summary>
        /// 対象の言語コードのリソースを参照し、stringの値を返します。
        /// </summary>
        /// <remarks>
        /// <see cref="LanguageChanger.GetText(string)"/>とした場合は文字列が長いため、<c>using static</c>等で<see cref="GetText(string)"/>単体で使用できるようにすることをお勧めします。
        /// </remarks>
        /// <param name="key">リソースを参照するキー。</param>
        /// <returns>対象の言語のリソース ファイルのキーから取得した値。</returns>
        public static string GetText(string key)
        {
            return Resource.ResourceManager.GetString(key, Resource.Culture);
        }
    }
}
