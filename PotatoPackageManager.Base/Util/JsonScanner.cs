using PotatoPackageManager.Base.Parser;
using System;

namespace PotatoPackageManager.Base.Util
{
    /// <summary>
    /// Json���X�L���������{���\�b�h�Q�ł��B<br />
    /// �\����̓p�^�[���͕ʂŎw�肵�܂��B
    /// </summary>
    public abstract class JsonScanner
    {
        /// <summary>
        /// �X�L���i��IText�C���^�[�t�F�[�X�ŏ��������܂��B
        /// </summary>
        /// <param name="itext">��{�e�L�X�g�������\�b�h�Q��񋟂���C���^�[�t�F�[�X�B</param>
        protected JsonScanner(IText itext)
        {
            Itext = itext;
            Scan = string.Empty;
            Current = Itext.Peek();
        }

        /// <summary>
        /// �C���^�[�t�F�[�X���v���p�e�B�Ƃ��ĕۑ����܂��B
        /// </summary>
        public IText Itext { get; }

        /// <summary>
        /// �X�L�������ĕϊ����s���Ă��镶����B
        /// </summary>
        public string Scan { get; private set; }

        /// <summary>
        /// ���݃X�L�������Ă��镶���B
        /// </summary>
        public char Current { get; set; }

        /// <summary>
        /// �󕶎���������܂��B
        /// </summary>
        public void Empty()
        {
            Scan = string.Empty;
        }

        /// <summary>
        /// ���݈ʒu���X�L�������܂��B
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public bool Check(Func<char, bool> func)
        {
            return func(Current);
        }

        /// <summary>
        /// ������<c>s</c>��<see cref="Scan"/>�ɒǉ����܂��B
        /// </summary>
        /// <param name="s">�ǉ����镶����B</param>
        public void Add(string s)
        {
            Scan += s;
        }

        /// <summary>
        /// �������i�߂܂��B
        /// </summary>
        public void Advance()
        {
            Current = Itext.AdvancePeek();
        }

        /// <summary>
        /// ���݂̕����񂪓K�؂ȏꍇ�ɒǉ����A���ɐi�߂܂��B
        /// </summary>
        public void AddAdvance()
        {
            Scan += Current;
            Current = Itext.AdvancePeek();
        }

        /// <summary>
        /// ���݂̕����񂪓K�؂ȏꍇ�ɍX�V���܂��B
        /// </summary>
        /// <param name="func">�X�L������ށB</param>
        /// <returns>�X�V�ł������ǂ����B</returns>
        public bool CheckAdvance(Func<char, bool> func)
        {
            if (!func(Current)) return false;
            Current = Itext.AdvancePeek();

            return true;
        }

        /// <summary>
        /// ���݈ʒu�ȍ~�̕�����ƈ�v����܂ŃX�L���i��i�߁A�X�V���܂��B
        /// </summary>
        /// <param name="s">��v�����镶����B</param>
        /// <returns>�X�L�����������������ǂ����B</returns>
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
        /// <see cref="CheckAdvance(Func{char, bool})"/>���ċA�I�ɍs���܂��B
        /// </summary>
        /// <param name="func">�X�L������ށB</param>
        /// <returns>�X�L�����������������ǂ����B</returns>
        public bool CheckAdvanceLoop(Func<char, bool> func)
        {
            var b = false;

            while (CheckAdvance(func))
                b = true;

            return b;
        }

        /// <summary>
        /// <see cref="CheckAdvance(Func{char, bool})"/>��ǉ��ƈꏏ�ɍs���܂��B
        /// </summary>
        /// <param name="func">�X�L������ށB</param>
        /// <returns>�X�L�����E�����������������ǂ����B</returns>
        public bool CheckAddAdvance(Func<char, bool> func)
        {
            if (!func(Current)) return false;
            Scan += Current;

            Current = Itext.AdvancePeek();

            return true;
        }

        /// <summary>
        /// <see cref="CheckAddAdvance(Func{char, bool})"/>���ċA�I�ɍs���܂��B
        /// </summary>
        /// <param name="func">�X�L������ށB</param>
        /// <returns>�X�L�����A�����������������ǂ����B</returns>
        public bool CheckAddAdvanceLoop(Func<char, bool> func)
        {
            var b = false;

            while (CheckAddAdvance(func))
                b = true;

            return b;
        }
    }
}
