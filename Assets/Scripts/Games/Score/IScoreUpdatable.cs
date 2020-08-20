namespace Games.Score
{
    /// <summary>
    /// スコアの更新を扱うinterface
    /// </summary>
    public interface IScoreUpdatable
    {
        /// <summary>
        /// スコアの更新
        /// </summary>
        void UpdateScore();
    }
}