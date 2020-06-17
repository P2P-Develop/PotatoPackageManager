using PotatoPackageManager.Base.Parser;
using System;
using System.Linq;

namespace PotatoPackageManager.Base.Exceptions
{
    /// <summary>
    /// �����}�b�v�̓ǂݍ��݂Ɏ��s�����ۂɃX���[�����O�B
    /// </summary>
    /// <remarks>
    /// ���̃N���X�͌p���ł��܂���B
    /// </remarks>
    public class FailedReadException : Exception
    {
        /// <summary>
        /// ��O�������ɕ\������郁�b�Z�[�W�B
        /// </summary>
        public override string Message { get; }

        /// <summary>
        /// ��O�����������܂��B
        /// </summary>
        public FailedReadException()
        {
            Message = "Failed to Read Argument Map.";
        }
    }

    /// <summary>
    /// �ʏ�e�L�X�g�����p�N���X�ł��B
    /// </summary>
    public class Text : IText
    {
        /// <summary>
        /// �e�L�X�g�����p�ɏ��������܂��B
        /// </summary>
        /// <param name="s">���̓e�L�X�g�B</param>
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
        /// ���݈ʒu��ۊǂ��܂��B
        /// </summary>
        /// <exception cref="FailedReadException">�s�ԍ����������ꍇ�ɗ�O�𔭂��܂��B</exception>
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
        /// �C���^�[�t�F�[�X�������s���܂��B
        /// </summary>
        /// <returns>���ݕ�����Ԃ��܂��B</returns>
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
        /// �C���^�[�t�F�[�X�������s���܂��B
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
