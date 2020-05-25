using System.Globalization;
using System.Threading;

namespace PotatoPackageManager.Base
{
    /// <summary>
    /// Manage multi-language environment.
    /// </summary>
    public class LanguageManager
    {
        /// <summary>
        /// Construct default language.
        /// </summary>
        /// <param name="lang">Default language code.<br>Example: ja-JP, en-US</br></param>
        public LanguageManager(string lang)
        {
            Resource.Culture = new CultureInfo(lang);
            Thread.CurrentThread.CurrentCulture = Resource.Culture;
            Thread.CurrentThread.CurrentUICulture = Resource.Culture;
        }

        /// <summary>
        /// Change default language to new language.
        /// </summary>
        /// <param name="afterlang">Language code for target language.</param>
        public static void ChangeLanguage(string afterlang)
        {
            Resource.Culture.ClearCachedData();
            Resource.Culture = null;
            Resource.Culture = new CultureInfo(afterlang);
        }

        /// <summary>
        /// Get text to selected language resources.
        /// </summary>
        /// <param name="key">a resource key.</param>
        /// <returns>selected language resource value.</returns>
        public static string GetText(string key)
        {
            return Resource.ResourceManager.GetString(key, Resource.Culture);
        }
    }
}
