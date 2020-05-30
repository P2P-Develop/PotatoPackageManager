using Xunit;
using PotatoPackageManager.Base.Parser;
using PotatoPackageManager.Base.Exceptions;

namespace PotatoPackageManager.Tests
{
    public class ArgsMapParseTest
    {
        [Fact(DisplayName = "Invalid argument map parse exception test")]
        public void ArgMapParseException()
        {
            _ = Assert.Throws<ArgumentMapParseException>(() =>
            {
                _ = ArgumentMapParser.Parse("}");
            });

            _ = Assert.Throws<ArgumentMapParseException>(() =>
            {
                _ = ArgumentMapParser.Parse(@"{ ""hoge"": ""fuga"" ""piyo"": ""hogera"" }");
            });

            _ = Assert.Throws<ArgumentMapParseException>(() =>
            {
                _ = ArgumentMapParser.Parse("{} {}");
            });

            _ = Assert.Throws<ArgumentMapParseException>(() =>
            {
                _ = ArgumentMapParser.Parse(@"{ ""hogehoge"" ""fugafuga"" }");
            });
        }
    }
}
