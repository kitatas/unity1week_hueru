using UniRx;

namespace Games.Score
{
    public sealed class ScorePresenter
    {
        private ScorePresenter(IScoreModel scoreModel, ScoreView scoreView)
        {
            scoreModel.Score
                .Subscribe(scoreView.UpdateScore)
                .AddTo(scoreView);
        }
    }
}