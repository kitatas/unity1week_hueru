using Games.Score;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Games.Buttons
{
    /// <summary>
    /// ランキングシーンを読み込むボタン
    /// </summary>
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(ButtonFader))]
    [RequireComponent(typeof(ButtonSpeaker))]
    public sealed class RankingButton : MonoBehaviour
    {
        private IScoreModel _scoreModel;
        private RankingLoader _rankingLoader;

        [Inject]
        private void Construct(IScoreModel scoreModel)
        {
            _scoreModel = scoreModel;
            _rankingLoader = RankingLoader.Instance;
        }

        private void Start()
        {
            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ => _rankingLoader.SendScoreAndShowRanking(_scoreModel.GetScore()))
                .AddTo(this);
        }
    }
}