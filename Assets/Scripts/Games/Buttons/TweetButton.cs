using Games.Score;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Games.Buttons
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(ButtonSpeaker))]
    public sealed class TweetButton : MonoBehaviour
    {
        [SerializeField] private GameObject effect = null;

        public Button Button { get; private set; }

        private readonly string _gameId = "huyasu_huyasu";

        private IScoreModel _scoreModel;

        [Inject]
        private void Construct(IScoreModel scoreModel)
        {
            _scoreModel = scoreModel;
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
                .Subscribe(_ =>
                {
                    var tweetText = $"{_scoreModel.GetScore()}個増やすことができた！";
                    UnityRoomTweet.Tweet(_gameId, tweetText);
                })
                .AddTo(this);
        }

        public void PlayEffect()
        {
            var position = new Vector3(-0.85f, 1.4f, 0f);
            Instantiate(effect, position, Quaternion.identity);
        }
    }
}