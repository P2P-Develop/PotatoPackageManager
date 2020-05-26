using Newtonsoft.Json.Linq;
using System.IO;

namespace PotatoPackageManager.Base.Parser
{
    /// <summary>
    /// ArgumentMapのJsonファイルをパースします。
    /// </summary>
    public class ArgumentMapParser
    {
        /// <summary>
        /// ArgumentMapのJsonを動的に分離します。<br/>
        /// ファイルを直接指定することはできません。<see cref="Stream"/>基底クラスを継承したクラス等から読み込んでから指定する必要があります。
        /// </summary>
        /// <param name="json">入力するJsonデータ。</param>
        /// <returns>Jsonのパースデータが含まれた動的型。</returns>
        public dynamic Parse(string json) => JObject.Parse(json);
    }
}