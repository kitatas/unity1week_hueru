using UniRx;

namespace Games.Score
{
    public interface IScoreModel
    {
        IReadOnlyReactiveProperty<int> Score { get; }
    }
}