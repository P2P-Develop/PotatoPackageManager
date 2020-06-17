using PotatoPackageManager.Base.Parser;
using System;

namespace PotatoPackageManager.Base.Util
{
    /// <summary>
    /// Jsonをスキャンする基本メソッド群です。<br />
    /// 構文解析パターンは別で指定します。
    /// </summary>
    public abstract class JsonScanner
    {
        /// <summary>
        /// スキャナをITextインターフェースで初期化します。
        /// </summary>
        /// <param name="itext">基本テキスト処理メソッド群を提供するインターフェース。</param>
        protected JsonScanner(IText itext)
        {
            Itext = itext;
            Scan = string.Empty;
            Current = Itext.Peek();
        }

        /// <summary>
        /// インターフェースをプロパティとして保存します。
        /// </summary>
        public IText Itext { get; }

        /// <summary>
        /// スキャンして変換を行っている文字列。
        /// </summary>
        public string Scan { get; private set; }

        /// <summary>
        /// 現在スキャンしている文字。
        /// </summary>
        public char Current { get; set; }

        /// <summary>
        /// 空文字列を代入します。
        /// </summary>
        public void Empty()
        {
            Scan = string.Empty;
        }

        /// <summary>
        /// 現在位置をスキャンします。
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public bool Check(Func<char, bool> func)
        {
            return func(Current);
        }

        /// <summary>
        /// 文字列<c>s</c>を<see cref="Scan"/>に追加します。
        /// </summary>
        /// <param name="s">追加する文字列。</param>
        public void Add(string s)
        {
            Scan += s;
        }

        /// <summary>
        /// 文字列を進めます。
        /// </summary>
        public void Advance()
        {
            Current = Itext.AdvancePeek();
        }

        /// <summary>
        /// 現在の文字列が適切な場合に追加し、次に進めます。
        /// </summary>
        public void AddAdvance()
        {
            Scan += Current;
            Current = Itext.AdvancePeek();
        }

        /// <summary>
        /// 現在の文字列が適切な場合に更新します。
        /// </summary>
        /// <param name="func">スキャン種類。</param>
        /// <returns>更新できたかどうか。</returns>
        public bool CheckAdvance(Func<char, bool> func)
        {
            if (!func(Current)) return false;
            Current = Itext.AdvancePeek();

            return true;
        }

        /// <summary>
        /// 現在位置以降の文字列と一致するまでスキャナを進め、更新します。
        /// </summary>
        /// <param name="s">一致させる文字列。</param>
        /// <returns>スキャンが成功したかどうか。</returns>
        public bool CheckAdvance(string s)
        {
            var i = 0;
            var i1 = i;
            bool Func(char _) => _ == s[i1];

            for (; i < s.Length; i++)
                if (!CheckAdvance(Func))
                    return false;

            return true;
        }

        /// <summary>
        /// <see cref="CheckAdvance(Func{char, bool})"/>を再帰的に行います。
        /// </summary>
        /// <param name="func">スキャン種類。</param>
        /// <returns>スキャンが成功したかどうか。</returns>
        public bool CheckAdvanceLoop(Func<char, bool> func)
        {
            var b = false;

            while (CheckAdvance(func))
                b = true;

            return b;
        }

        /// <summary>
        /// <see cref="CheckAdvance(Func{char, bool})"/>を追加と一緒に行います。
        /// </summary>
        /// <param name="func">スキャン種類。</param>
        /// <returns>スキャン・処理が成功したかどうか。</returns>
        public bool CheckAddAdvance(Func<char, bool> func)
        {
            if (!func(Current)) return false;
            Scan += Current;

            Current = Itext.AdvancePeek();

            return true;
        }

        /// <summary>
        /// <see cref="CheckAddAdvance(Func{char, bool})"/>を再帰的に行います。
        /// </summary>
        /// <param name="func">スキャン種類。</param>
        /// <returns>スキャン、処理が成功したかどうか。</returns>
        public bool CheckAddAdvanceLoop(Func<char, bool> func)
        {
            var b = false;

            while (CheckAddAdvance(func))
                b = true;

            return b;
        }
    }
}
