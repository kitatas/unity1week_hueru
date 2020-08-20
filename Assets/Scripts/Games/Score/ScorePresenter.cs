using UniRx;

namespace Games.Score
{
    /// <summary>
    /// スコアのPresenter
    /// </summary>
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