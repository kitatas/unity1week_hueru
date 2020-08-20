using UniRx;

namespace Games.Score
{
    /// <summary>
    /// スコアのModel
    /// </summary>
    public sealed class ScoreModel : IScoreModel, IScoreUpdatable
    {
        private readonly ReactiveProperty<int> _score;

        public ScoreModel()
        {
            _score = new ReactiveProperty<int>(0);
        }

        /// <summary>
        /// スコアのReactiveProperty
        /// </summary>
        public IReadOnlyReactiveProperty<int> Score => _score;

        /// <summary>
        /// スコアの値
        /// </summary>
        /// <returns></returns>
        public int GetScore() => _score.Value;

        /// <summary>
        /// スコアの更新
        /// </summary>
        public void UpdateScore()
        {
            _score.Value++;
        }
    }
}