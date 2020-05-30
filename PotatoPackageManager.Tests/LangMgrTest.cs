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
                LanguageManager lang = new LanguageManager("hoge-FUGA");
            });

            Assert.True(ex.Message == "Language Code \"hoge-FUGA\" invalid.");
        }

        [Fact(DisplayName = "LanguageManager ChangeLanguage() Exception test")]
        public void LangMgrChLangException()
        {
            LanguageManager lang = new LanguageManager("ja-JP");

            InvalidLanguageException ex = Assert.Throws<InvalidLanguageException>(() =>
            {
                LanguageManager.ChangeLanguage("foo-BAR");
            });

            Assert.True(ex.Message == "Language Code \"foo-BAR\" invalid.");
        }
    }
}
