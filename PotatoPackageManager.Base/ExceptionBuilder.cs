using System;
using static PotatoPackageManager.Base.LanguageChanger;
using static PotatoPackageManager.Base.Util.Logger;

namespace PotatoPackageManager.Base
{
    /// <summary>
    /// Exceptionのスローを管理します。
    /// </summary>
    public static class ExceptionBuilder
    {
        /// <summary>
        /// <see cref="Exception"/>基底クラス継承先のExceptionのメッセージを含めてスローします。
        /// </summary>
        /// <param name="ex">Exceptionのメッセージ参照に使用します。</param>
        /// <param name="suggesthelp">ヘルプの参照を勧めるかどうか。</param>
        public static void Throw(Exception ex, bool suggesthelp)
        {
            Error(ex.Message + (suggesthelp ? "" : " " + GetText("SuggestionHelp")));
        }
    }
}
