using System;
using static PotatoPackageManager.Base.LanguageManager;

namespace PotatoPackageManager.Base.Exceptions
{
    /// <summary>
    /// 引数マップのパースに失敗した時にスローするクラス。
    /// </summary>
    [Serializable]
    public class ArgumentMapParseException : Exception
    {
        /// <summary>
        /// 例外の初期化をします。
        /// </summary>
        public ArgumentMapParseException()
        {
            Message = GetText("ArgumentMapParseError");
            ArgsMap = GetText("Unknown");
        }

        /// <summary>
        /// ユーザー向けエラー内容。
        /// </summary>
        public override string Message { get; }

        /// <summary>
        /// 引数マップのエラー部分。
        /// </summary>
        public virtual string ArgsMap { get; private set; }

        /// <summary>
        /// 通常のメッセージでエラーをスローします。
        /// </summary>
        /// <param name="argsmap">エラー部分の引数マップ内容。</param>
        public ArgumentMapParseException(string argsmap)
        {
            ArgsMap = argsmap;
        }
    }
}
