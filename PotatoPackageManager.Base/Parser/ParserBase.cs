namespace PotatoPackageManager.Base.Parser
{
    /// <summary>
    /// �ꏊ����ۊǂ���N���X�B
    /// </summary>
    public class PositionInfo
    {
        /// <summary>
        /// �N���X�ɓ����ۊǏ������������܂��B
        /// </summary>
        /// <param name="line">���ݍs�̕�����B</param>
        /// <param name="lineNumber">���ݍs�ԍ��B</param>
        /// <param name="linePosition">���ݗ�ԍ��B</param>
        public PositionInfo(string line, int lineNumber, int linePosition)
        {
            Line = line;
            LineNumber = lineNumber;
            LinePosition = linePosition;
        }

        /// <summary>
        /// ���ݍs�̕�����B
        /// </summary>
        public string Line { get; }

        /// <summary>
        /// ���ݍs�ԍ��B
        /// </summary>
        public int LineNumber { get; }

        /// <summary>
        /// ���ݗ�ԍ��B
        /// </summary>
        public int LinePosition { get; }
    }

    /// <summary>
    /// �e�L�X�g����p�̊�{���\�b�h��񋟂��܂��B
    /// </summary>
    public interface IText
    {
        /// <summary>
        /// ���݈ʒu������肵�܂��B
        /// </summary>
        PositionInfo PositionInfo { get; }

        /// <summary>
        /// ���݈ʒu�̕�����Ԃ��܂��B
        /// </summary>
        /// <returns></returns>
        char Peek();

        /// <summary>
        /// ���݈ʒu���ꕶ���i�߂܂�
        /// </summary>
        void Advance();
    }

    /// <summary>
    /// �e�L�X�g����p�̊�{���\�b�h��֗��ɂ����h�����\�b�h��񋟂��܂��B
    /// </summary>
    public static class TextExtension
    {
        /// <summary>
        /// ���ɕ�����i�߂Ă��̕������擾���܂��B
        /// </summary>
        /// <param name="itext">�e�L�X�g����C���^�[�t�F�[�X�B</param>
        /// <returns>������i�߂���̕�����Ԃ��܂��B</returns>
        public static char AdvancePeek(this IText itext)
        {
            itext.Advance();

            return itext.Peek();
        }
    }
}
