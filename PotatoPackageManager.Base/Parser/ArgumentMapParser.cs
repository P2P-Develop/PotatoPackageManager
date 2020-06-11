using Newtonsoft.Json.Linq;
using PotatoPackageManager.Base.Exceptions;
using System;
using System.IO;

namespace PotatoPackageManager.Base.Parser
{
    /// <summary>
    /// ArgumentMapのJsonファイルをパースします。
    /// </summary>
    public static class ArgumentMapParser
    {
        /// <summary>
        /// ArgumentMapのJsonを動的に分離します。<br/>
        /// ファイルを直接指定することはできません。<see cref="Stream"/>基底クラスを継承したクラス等から読み込んでから指定する必要があります。
        /// </summary>
        /// <param name="json">入力するJsonデータ。</param>
        /// <returns>Jsonのパースデータが含まれた動的型。</returns>
        public static dynamic Parse(string json)
        {
            try
            {
                return JObject.Parse(json);
            }
            catch (Exception)
            {
                throw new ArgumentMapParseException(json);
            }
        }
    }
}
