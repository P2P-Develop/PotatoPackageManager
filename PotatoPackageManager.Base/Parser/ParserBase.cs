namespace PotatoPackageManager.Base.Parser
{
    /// <summary>
    /// 場所情報を保管するクラス。
    /// </summary>
    public class PositionInfo
    {
        /// <summary>
        /// クラスに入れる保管情報を初期化します。
        /// </summary>
        /// <param name="line">現在行の文字列。</param>
        /// <param name="lineNumber">現在行番号。</param>
        /// <param name="linePosition">現在列番号。</param>
        public PositionInfo(string line, int lineNumber, int linePosition)
        {
            Line = line;
            LineNumber = lineNumber;
            LinePosition = linePosition;
        }

        /// <summary>
        /// 現在行の文字列。
        /// </summary>
        public string Line { get; }

        /// <summary>
        /// 現在行番号。
        /// </summary>
        public int LineNumber { get; }

        /// <summary>
        /// 現在列番号。
        /// </summary>
        public int LinePosition { get; }
    }

    /// <summary>
    /// テキスト制御用の基本メソッドを提供します。
    /// </summary>
    public interface IText
    {
        /// <summary>
        /// 現在位置情報を入手します。
        /// </summary>
        PositionInfo PositionInfo { get; }

        /// <summary>
        /// 現在位置の文字を返します。
        /// </summary>
        /// <returns></returns>
        char Peek();

        /// <summary>
        /// 現在位置を一文字進めます
        /// </summary>
        void Advance();
    }

    /// <summary>
    /// テキスト制御用の基本メソッドを便利にした派生メソッドを提供します。
    /// </summary>
    public static class TextExtension
    {
        /// <summary>
        /// 次に文字を進めてその文字を取得します。
        /// </summary>
        /// <param name="itext">テキスト制御インターフェース。</param>
        /// <returns>文字を進めた後の文字を返します。</returns>
        public static char AdvancePeek(this IText itext)
        {
            itext.Advance();

            return itext.Peek();
        }
    }
}
