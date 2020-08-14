using Games.Score;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Games.Buttons
{
    [RequireComponent(typeof(Button))]
    public sealed class RankingButton : MonoBehaviour
    {
        public Button Button { get; private set; }

        private IScoreModel _scoreModel;
        private RankingLoader _rankingLoader;

        [Inject]
        private void Construct(IScoreModel scoreModel)
        {
            _scoreModel = scoreModel;
            _rankingLoader = RankingLoader.Instance;
        }

        private void Awake()
        {
            Button = GetComponent<Button>();
            Button.enabled = false;
        }

        private void Start()
        {
            Button
                .OnClickAsObservable()
                .Subscribe(_ => _rankingLoader.SendScoreAndShowRanking(_scoreModel.GetScore()))
                .AddTo(this);
        }
    }
}