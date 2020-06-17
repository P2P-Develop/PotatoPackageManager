using PotatoPackageManager.Base.Parser;
using System;
using static PotatoPackageManager.Base.LanguageChanger;

namespace PotatoPackageManager.Base.Exceptions
{
    /// <summary>
    /// 引数マップのパースに失敗した時にスローするクラス。
    /// </summary>
    public class ArgumentMapParseException : Exception
    {
        /// <summary>
        /// 例外の初期化をします。
        /// </summary>
        public ArgumentMapParseException()
        {
            Message = GetText("ArgumentMapParseError");
            ArgsMap = null;
        }

        /// <summary>
        /// ユーザー向けエラー内容。
        /// </summary>
        public override string Message { get; }

        /// <summary>
        /// 引数マップのエラー部分。
        /// </summary>
        public PositionInfo ArgsMap { get; private set; }

        /// <summary>
        /// 通常のメッセージでエラーをスローします。
        /// </summary>
        /// <param name="psinfo">エラー部分の引数マップ内容近くの<see cref="PositionInfo" />クラス情報。</param>
        public ArgumentMapParseException(PositionInfo psinfo)
        {
            Message = GetText("ArgumentMapParseError");
            ArgsMap = psinfo;
        }

        /// <summary>
        /// メッセージ付きでエラーをスローします。
        /// </summary>
        /// <param name="message">ユーザーに表示するべきメッセージ。</param>
        /// <param name="psinfo">エラー部分の引数マップ内近くの<see cref="PositionInfo" />クラス情報。</param>
        public ArgumentMapParseException(string message, PositionInfo psinfo) : base(message)
        {
            Message = message;
            ArgsMap = psinfo;
        }
    }
}
