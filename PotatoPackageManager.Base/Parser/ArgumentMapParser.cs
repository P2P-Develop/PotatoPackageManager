using Newtonsoft.Json.Linq;
using PotatoPackageManager.Base.Exceptions;
using PotatoPackageManager.Base.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PotatoPackageManager.Base.Parser
{
    /// <summary>
    /// ArgumentMapのJsonファイルをパースします。
    /// 静的型または動的型でパースすることが出来ます。
    /// </summary>
    public static class ArgumentMapParser
    {
        #region Character Enumerator Functions

        private static readonly Func<char, bool> IsSign = c => c == '-' || c == '+';
        private static readonly Func<char, bool> IsDigit = c => c.IsDigit();
        private static readonly Func<char, bool> IsDecimalPoint = c => c == '.';
        private static readonly Func<char, bool> IsExponent = c => c == 'e' || c == 'E';
        private static readonly Func<char, bool> IsDoubleQuote = c => c == '"';
        private static readonly Func<char, bool> IsBackslash = c => c == '\\';
        private static readonly Func<char, bool> IsSlash = c => c == '/';
        private static readonly Func<char, bool> IsSmallB = c => c == 'b';
        private static readonly Func<char, bool> IsSmallF = c => c == 'f';
        private static readonly Func<char, bool> IsSmallN = c => c == 'n';
        private static readonly Func<char, bool> IsSmallR = c => c == 'r';
        private static readonly Func<char, bool> IsSmallT = c => c == 't';
        private static readonly Func<char, bool> IsSmallU = c => c == 'u';
        private static readonly Func<char, bool> IsControl = c => c.IsControl();
        private static readonly Func<char, bool> IsWhitespace = c => c.IsWhitespace();
        private static readonly Func<char, bool> IsLeftBracket = c => c == '[';
        private static readonly Func<char, bool> IsRightBracket = c => c == ']';
        private static readonly Func<char, bool> IsComma = c => c == ',';
        private static readonly Func<char, bool> IsLeftBrace = c => c == '{';
        private static readonly Func<char, bool> IsRightBrace = c => c == '}';
        private static readonly Func<char, bool> IsColon = c => c == ':';

        #endregion Character Enumerator Functions

        /// <summary>
        /// 数字をパースします。これは<see cref="Parse(JsonScanner)" />の一部で使用されます。
        /// </summary>
        /// <param name="scanner">指定するスキャナ。</param>
        /// <returns>パースに成功した場合32ビット符号付き数字を返します。</returns>
        public static int ParseNumber(JsonScanner scanner)
        {
            scanner.Empty();

            scanner.CheckAddAdvance(IsSign);

            scanner.CheckAddAdvanceLoop(IsDigit);

            scanner.CheckAddAdvance(IsDecimalPoint);

            scanner.CheckAddAdvanceLoop(IsDigit);

            if (scanner.CheckAddAdvance(IsExponent))
            {
                scanner.CheckAddAdvance(IsSign);

                scanner.CheckAddAdvanceLoop(IsDigit);
            }

            try
            {
                return int.Parse(scanner.Scan);
            }
            catch (FormatException)
            {
                throw new ArgumentMapParseException("bad format number", scanner.Itext.PositionInfo);
            }
            catch (OverflowException)
            {
                throw new ArgumentMapParseException("overflow number", scanner.Itext.PositionInfo);
            }
            catch (Exception)
            {
                throw new ArgumentMapParseException("unexpected error number", scanner.Itext.PositionInfo);
            }
        }

        /// <summary>
        /// Json文字列をパースします。これは<see cref="Parse(JsonScanner)"/>の一部で使用されます。
        /// </summary>
        /// <param name="scanner">指定するスキャナ。</param>
        /// <returns>パースに成功した場合文字列をさらに返します。</returns>
        private static string ParseString(JsonScanner scanner)
        {
            const string b = "\b";
            const string f = "\f";
            const string n = "\n";
            const string r = "\r";
            const string t = "\t";

            scanner.Empty();

            if (!scanner.CheckAdvance(IsDoubleQuote))
                throw new ArgumentMapParseException("string -> double quotation mark required", scanner.Itext.PositionInfo);

            while (!scanner.CheckAdvance(IsDoubleQuote))
            {
                if (scanner.CheckAdvance(IsBackslash))
                {
                    if (scanner.CheckAddAdvance(IsDoubleQuote))
                        continue;
                    if (scanner.CheckAddAdvance(IsBackslash))
                        continue;
                    if (scanner.CheckAddAdvance(IsSlash))
                        continue;
                    if (scanner.CheckAdvance(IsSmallB))
                        scanner.Add(b);
                    else if (scanner.CheckAdvance(IsSmallF))
                        scanner.Add(f);
                    else if (scanner.CheckAdvance(IsSmallN))
                        scanner.Add(n);
                    else if (scanner.CheckAdvance(IsSmallR))
                        scanner.Add(r);
                    else if (scanner.CheckAdvance(IsSmallT))
                        scanner.Add(t);
                    else if (scanner.CheckAdvance(IsSmallU))
                    {
                        var codepoint = 0;

                        for (var i = 0; i < 4; i++)
                            try
                            {
                                codepoint = codepoint * 16 + scanner.Current.ToHexValue();

                                scanner.Advance();
                            }
                            catch (Exception)
                            {
                                break;
                            }

                        scanner.Add(Convert.ToChar(codepoint).ToString());
                    }
                    else
                        throw new ArgumentMapParseException("string -> not supported escape", scanner.Itext.PositionInfo);
                }
                else if (scanner.Check(IsControl))
                    throw new ArgumentMapParseException("string -> control character", scanner.Itext.PositionInfo);
                else
                    scanner.AddAdvance();
            }

            return scanner.Scan;
        }

        /// <summary>
        /// 空白をパースします。これは<see cref="Parse(JsonScanner)" />や一部のメソッドで使用されます。
        /// </summary>
        /// <param name="scanner">指定するスキャナ。</param>
        private static void ParseWhitespace(JsonScanner scanner)
        {
            scanner.CheckAdvanceLoop(IsWhitespace);
        }

        /// <summary>
        /// 配列をパースします。これは<see cref="Parse(JsonScanner)" />や一部のメソッドで使用されます。
        /// </summary>
        /// <param name="scanner">指定するスキャナ。</param>
        /// <returns>パースに成功した場合<see cref="object" />の配列を返します。 <br />
        /// 何かの配列として利用するにはその他の処理が必要です。</returns>
        private static object[] ParseArray(JsonScanner scanner)
        {
            var osList = new List<object>();

            if (!scanner.CheckAdvance(IsLeftBracket))
                throw new ArgumentMapParseException("array -> left bracket required", scanner.Itext.PositionInfo);

            ParseWhitespace(scanner);

            if (scanner.CheckAdvance(IsRightBracket))
                return osList.ToArray();

            while (true)
            {
                osList.Add(ParseValue(scanner));

                ParseWhitespace(scanner);

                if (scanner.CheckAdvance(IsRightBracket))
                    return osList.ToArray();
                if (!scanner.CheckAdvance(IsComma))
                    throw new ArgumentMapParseException("array -> comma required", scanner.Itext.PositionInfo);

                ParseWhitespace(scanner);
            }
        }

        /// <summary>
        /// Json値の入れ子となるオブジェクトをパースします。<see cref="Parse(JsonScanner)" />の一部で使用されます。
        /// </summary>
        /// <param name="scanner">指定するスキャナ。</param>
        /// <returns><see cref="string" />にキー値を代入してハッシュマップで返します。</returns>
        private static Dictionary<string, object> ParseObject(JsonScanner scanner)
        {
            var osDict = new Dictionary<string, object>();

            if (!scanner.CheckAdvance(IsLeftBrace))
                throw new ArgumentMapParseException("object -> left brace required", scanner.Itext.PositionInfo);

            ParseWhitespace(scanner);

            if (scanner.CheckAdvance(IsRightBrace))
                return osDict;

            while (true)
            {
                var key = ParseString(scanner);

                if (osDict.Keys.Contains(key))
                    throw new ArgumentMapParseException("object -> same key", scanner.Itext.PositionInfo);

                ParseWhitespace(scanner);

                if (!scanner.CheckAdvance(IsColon))
                    throw new ArgumentMapParseException("object -> colon required", scanner.Itext.PositionInfo);

                ParseWhitespace(scanner);

                osDict[key] = ParseValue(scanner);

                ParseWhitespace(scanner);

                if (scanner.CheckAdvance(IsRightBrace))
                    return osDict;

                if (!scanner.CheckAdvance(IsComma))
                    throw new ArgumentMapParseException("object -> comma required", scanner.Itext.PositionInfo);

                ParseWhitespace(scanner);
            }
        }

        /// <summary>
        /// 予約語(<c>true</c>, <c>false</c>, <c>null</c>)をパースします。<see cref="Parse(JsonScanner)" />で使用されます。
        /// </summary>
        /// <param name="scanner">指定するスキャナ。</param>
        /// <returns>予約語をそのまま返します。</returns>
        private static object ParseWord(JsonScanner scanner)
        {
            if (scanner.Check(IsSmallT))
            {
                if (!scanner.CheckAdvance("true"))
                    throw new ArgumentMapParseException("word -> true required", scanner.Itext.PositionInfo);

                return true;
            }

            if (scanner.Check(IsSmallF))
            {
                if (!scanner.CheckAdvance("false"))
                    throw new ArgumentMapParseException("word -> false required", scanner.Itext.PositionInfo);

                return false;
            }

            if (!scanner.Check(IsSmallN))
                throw new ArgumentMapParseException("word -> unexpected character", scanner.Itext.PositionInfo);
            if (!scanner.CheckAdvance("null"))
                throw new ArgumentMapParseException("word -> null required", scanner.Itext.PositionInfo);

            return null;
        }

        /// <summary>
        /// 今までのパース引数を構文解析と共にまとめます。
        /// </summary>
        /// <param name="scanner">指定するスキャナ。</param>
        /// <returns>objectとして<see cref="Parse(JsonScanner)" />に渡します。</returns>
        private static object ParseValue(JsonScanner scanner)
        {
            ParseWhitespace(scanner);

            return scanner.Check(IsLeftBrace) ? ParseObject(scanner) :
                scanner.Check(IsLeftBracket) ? ParseArray(scanner) :
                scanner.Check(IsDoubleQuote) ? ParseString(scanner) :
                scanner.Check(IsSign) ? ParseNumber(scanner) :
                scanner.Check(IsDigit) ? ParseNumber(scanner) : ParseWord(scanner);
        }

        /// <summary>
        /// Jsonを静的にパースします。
        /// </summary>
        /// <param name="scanner">指定するスキャナ。</param>
        /// <returns>objectとして値を返します。Dictionaryとして代入するのが最適です。</returns>
        public static object Parse(JsonScanner scanner) => ParseValue(scanner);

        /// <summary>
        /// Jsonを動的にパースします。
        /// IntelliSenseなどの補完機能の恩恵が得られなくなりますが、最も素早い方法でもあります。
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static dynamic ParseDynamic(string json) => JObject.Parse(json);
    }
}
