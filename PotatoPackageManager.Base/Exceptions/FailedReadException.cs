using PotatoPackageManager.Base.Parser;
using System;
using System.Linq;

namespace PotatoPackageManager.Base.Exceptions
{
    /// <summary>
    /// 引数マップの読み込みに失敗した際にスローする例外。
    /// </summary>
    /// <remarks>
    /// このクラスは継承できません。
    /// </remarks>
    public class FailedReadException : Exception
    {
        /// <summary>
        /// 例外処理時に表示されるメッセージ。
        /// </summary>
        public override string Message { get; }

        /// <summary>
        /// 例外を初期化します。
        /// </summary>
        public FailedReadException()
        {
            Message = "Failed to Read Argument Map.";
        }
    }

    /// <summary>
    /// 通常テキスト処理用クラスです。
    /// </summary>
    public class Text : IText
    {
        /// <summary>
        /// テキスト処理用に初期化します。
        /// </summary>
        /// <param name="s">入力テキスト。</param>
        public Text(string s)
        {
            const string nullchar = "\0";

            if (!s.EndsWith(nullchar))
                s += nullchar;

            _lines = s.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Select(elem => elem + Environment.NewLine).ToArray();
            _lineNumber = 0;
            _linePosition = 0;
        }

        private readonly string[] _lines;
        private int _lineNumber;
        private int _linePosition;

        /// <summary>
        /// 現在位置を保管します。
        /// </summary>
        /// <exception cref="FailedReadException">行番号が超えた場合に例外を発します。</exception>
        public PositionInfo PositionInfo
        {
            get
            {
                try
                {
                    return new PositionInfo(_lines[_lineNumber], _lineNumber, _linePosition);
                }
                catch (IndexOutOfRangeException)
                {
                    throw new FailedReadException();
                }
            }
        }

        /// <summary>
        /// インターフェース実装を行います。
        /// </summary>
        /// <returns>現在文字を返します。</returns>
        public char Peek()
        {
            try
            {
                return _lines[_lineNumber][_linePosition];
            }
            catch (IndexOutOfRangeException)
            {
                throw new FailedReadException();
            }
        }

        /// <summary>
        /// インターフェース実装を行います。
        /// </summary>
        public void Advance()
        {
            try
            {
                if (_linePosition == _lines[_lineNumber].Length - 1)
                {
                    _lineNumber++;
                    _linePosition = 0;
                }
                else
                    _linePosition++;
            }
            catch (IndexOutOfRangeException)
            {
                throw new FailedReadException();
            }
        }
    }
}
