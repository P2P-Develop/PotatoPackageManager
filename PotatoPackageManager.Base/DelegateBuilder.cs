namespace PotatoPackageManager.Base
{
    /// <summary>
    /// 指定されたアクションを定義・実行します。
    /// </summary>
    public static class DelegateBuilder
    {
        /// <summary>
        /// パッケージリストをアップデートします。<br/>
        /// パッケージリストが既に最新の場合はアップデートされませんが、<c>force</c>が<c>true</c>になっている場合のみ最新であっても強制的にアップデートします。
        /// </summary>
        /// <param name="force">強制的にパッケージリストをアップデートするかどうか。</param>
        /// <param name="pkglists">アップグレードするリポジトリを選択します。<br/>
        /// この引数は省略可能です。</param>
        public delegate void Update(bool force, string[] pkglists = null);

        /// <summary>
        /// アップデートしたパッケージリストからアップグレードができるパッケージがあるか確認します。<br/>
        /// アップグレードできるパッケージがある場合はユーザーにアップグレード確認メッセージを表示しアップグレードします。
        /// </summary>
        /// <param name="noask">ユーザーへの確認メッセージを表示せずアップグレードを実行するかどうか。</param>
        public delegate void Upgrade(bool noask);
    }
}
