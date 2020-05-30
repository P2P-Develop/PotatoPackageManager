using Xunit;
using PotatoPackageManager.Base;
using PotatoPackageManager.Base.Exceptions;

namespace PotatoPackageManager.Tests
{
    public class LangMgrTest
    {
        [Fact(DisplayName = "LanguageManager new instance Exception test")]
        public void LangMgrNewException()
        {
            InvalidLanguageException ex = Assert.Throws<InvalidLanguageException>(() =>
            {
                LanguageManager lang = new LanguageManager("hogeFUGA");
            });

            Assert.True(ex.Message == "Language Code \"hogeFUGA\" invalid.");
        }

        [Fact(DisplayName = "LanguageManager ChangeLanguage() Exception test")]
        public void LangMgrChLangException()
        {
            LanguageManager lang = new LanguageManager("ja-JP");

            InvalidLanguageException ex = Assert.Throws<InvalidLanguageException>(() =>
            {
                LanguageManager.ChangeLanguage("fooBAR");
            });

            Assert.True(ex.Message == "Language Code \"fooBAR\" invalid.");
        }
    }
}
