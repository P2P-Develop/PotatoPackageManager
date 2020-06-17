using System;

namespace PotatoPackageManager.Base.Util
{
    /// <summary>
    /// �����ɑ΂��郆�[�e�B���e�B��񋟂��܂��B
    /// </summary>
    public static class CharExtensions
    {
        /// <summary>
        /// �����ɂ������������䕶���ł��邩�m�F���܂��B
        /// </summary>
        /// <param name="c">���͂��镶���B</param>
        /// <returns>���䕶���ł���ꍇ��<c>true</c>��Ԃ��܂��B</returns>
        public static bool IsControl(this char c) => c >= 0 && c <= 0x1F;

        /// <summary>
        /// �����ɂ����������X�y�[�X�ł��邩�m�F���܂��B
        /// </summary>
        /// <param name="c">���͂��镶���B</param>
        /// <returns>�X�y�[�X�ł���ꍇ��<c>true</c>��Ԃ��܂��B</returns>
        public static bool IsWhitespace(this char c) => c == ' ' || c == '\t' || c == '\n' || c == '\r';

        /// <summary>
        /// �����ɂ����������ԍ��ł��邩�m�F���܂��B
        /// </summary>
        /// <param name="c">���͂��镶���B</param>
        /// <returns>�ԍ��ł���ꍇ��<c>true</c>��Ԃ��܂��B</returns>
        public static bool IsDigit(this char c) => c >= '0' && c <= '9';

        /// <summary>
        /// �����ɂ���������16�i���Ƃ��Đ��藧�����ł��邩�m�F���܂��B
        /// </summary>
        /// <param name="c">���͂��镶���B</param>
        /// <returns>16�i���Ƃ��Đ��藧�����ł���ꍇ��<c>true</c>��Ԃ��܂��B</returns>
        public static bool IsHexDigit(this char c) => (c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f');

        /// <summary>
        /// �����ɂ���������16�i���Ƃ��Đ��藧�����ł���ꍇ��16�i����<see cref="int"/>�ɕϊ����܂��B
        /// </summary>
        /// <param name="c">���͂��镶���B</param>
        /// <returns>16�i���Ƃ��đ������<see cref="int"/>�^�����B</returns>
        public static int ToHexValue(this char c) =>
            !c.IsHexDigit() ? throw new Exception() :
            c >= '0' && c <= '9' ? c - '0' : char.ToUpper(c) - 'A' + 10;
    }
}
