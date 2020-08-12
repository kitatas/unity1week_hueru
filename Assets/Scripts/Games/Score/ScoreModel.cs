using UniRx;

namespace Games.Score
{
    public sealed class ScoreModel : IScoreModel, IScoreUpdatable
    {
        private readonly ReactiveProperty<int> _score;

        public ScoreModel()
        {
            _score = new ReactiveProperty<int>(0);
        }

        public IReadOnlyReactiveProperty<int> Score => _score;

        public void UpdateScore()
        {
            _score.Value++;
        }
    }
}