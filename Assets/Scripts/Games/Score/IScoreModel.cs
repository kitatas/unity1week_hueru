using UniRx;

namespace Games.Score
{
    /// <summary>
    /// スコアの値を扱うinterface
    /// </summary>
    public interface IScoreModel
    {
        /// <summary>
        /// スコアのReactiveProperty
        /// </summary>
        IReadOnlyReactiveProperty<int> Score { get; }

        /// <summary>
        /// スコアの値
        /// </summary>
        /// <returns></returns>
        int GetScore();
    }
}