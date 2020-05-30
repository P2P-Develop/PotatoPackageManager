using System;
using System.Globalization;

namespace PotatoPackageManager.Base.Exceptions
{
    /// <summary>
    /// 言語コードが見つからなかった場合にスローするクラス。
    /// </summary>
    /// <remarks>
    /// このクラスは継承できません。
    /// </remarks>
    [Serializable]
    public class InvalidLanguageException : CultureNotFoundException
    {
        /// <summary>
        /// ユーザー向けエラー内容。
        /// </summary>
        public override string Message { get; } = "Language Code invalid.";

        /// <summary>
        /// 無効な言語コードの値。
        /// </summary>
        public virtual string Langcode { get; private set; }

        /// <summary>
        /// 通常のメッセージでエラーをスローします。
        /// </summary>
        /// <param name="langcode">無効な言語コードの値。</param>
        public InvalidLanguageException(string langcode)
        {
            Langcode = langcode;
            Message = "Language Code \"" + Langcode + "\" invalid.";
        }
    }
}
