using System;

namespace PotatoPackageManager.Base.Util
{
    /// <summary>
    /// 文字に対するユーティリティを提供します。
    /// </summary>
    public static class CharExtensions
    {
        /// <summary>
        /// 引数にした文字が制御文字であるか確認します。
        /// </summary>
        /// <param name="c">入力する文字。</param>
        /// <returns>制御文字である場合に<c>true</c>を返します。</returns>
        public static bool IsControl(this char c) => c >= 0 && c <= 0x1F;

        /// <summary>
        /// 引数にした文字がスペースであるか確認します。
        /// </summary>
        /// <param name="c">入力する文字。</param>
        /// <returns>スペースである場合に<c>true</c>を返します。</returns>
        public static bool IsWhitespace(this char c) => c == ' ' || c == '\t' || c == '\n' || c == '\r';

        /// <summary>
        /// 引数にした文字が番号であるか確認します。
        /// </summary>
        /// <param name="c">入力する文字。</param>
        /// <returns>番号である場合に<c>true</c>を返します。</returns>
        public static bool IsDigit(this char c) => c >= '0' && c <= '9';

        /// <summary>
        /// 引数にした文字が16進数として成り立つ文字であるか確認します。
        /// </summary>
        /// <param name="c">入力する文字。</param>
        /// <returns>16進数として成り立つ文字である場合に<c>true</c>を返します。</returns>
        public static bool IsHexDigit(this char c) => (c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f');

        /// <summary>
        /// 引数にした文字が16進数として成り立つ文字である場合に16進数の<see cref="int"/>に変換します。
        /// </summary>
        /// <param name="c">入力する文字。</param>
        /// <returns>16進数として代入した<see cref="int"/>型数字。</returns>
        public static int ToHexValue(this char c) =>
            !c.IsHexDigit() ? throw new Exception() :
            c >= '0' && c <= '9' ? c - '0' : char.ToUpper(c) - 'A' + 10;
    }
}
