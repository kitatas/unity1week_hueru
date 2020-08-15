using Games.Score;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Games.Buttons
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(ButtonSpeaker))]
    public sealed class RankingButton : MonoBehaviour
    {
        [SerializeField] private GameObject effect = null;

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

        public void PlayEffect()
        {
            var position = new Vector3(0.85f, 1.4f, 0f);
            Instantiate(effect, position, Quaternion.identity);
        }
    }
}