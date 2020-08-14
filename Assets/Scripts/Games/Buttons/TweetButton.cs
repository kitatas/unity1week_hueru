using Games.Score;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Games.Buttons
{
    [RequireComponent(typeof(Button))]
    public sealed class TweetButton : MonoBehaviour
    {
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
    }
}