using System;
using static PotatoPackageManager.Base.LanguageManager;

namespace PotatoPackageManager.Base
{
    /// <summary>
    /// Exceptionのスローを管理します。
    /// </summary>
    public class ExceptionManager
    {
        /// <summary>
        /// <see cref="Exception"/>基底クラス継承先のExceptionのメッセージを含めてスローします。
        /// </summary>
        /// <param name="ex">Exceptionのメッセージ参照に使用します。</param>
        /// <param name="suggesthelp">ヘルプの参照を勧めるかどうか。</param>
        public static void Throw(Exception ex, bool suggesthelp)
        {
            Logger.Error(ex.Message + (suggesthelp ? "" : " " + GetText("SuggestionHelp")));
        }
    }
}