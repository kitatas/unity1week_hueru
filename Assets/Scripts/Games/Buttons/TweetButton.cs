using Games.Score;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using Zenject;

namespace Games.Buttons
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(ButtonSpeaker))]
    public sealed class TweetButton : MonoBehaviour
    {
        [SerializeField] private GameObject effect = null;

        private readonly string _gameId = "huyasu_huyasu";
        private readonly string _hashTag1 = "unityroom";
        private readonly string _hashTag2 = "unity1week";

        private Button _button;
        private IScoreModel _scoreModel;

        [Inject]
        private void Construct(IScoreModel scoreModel)
        {
            _button = GetComponent<Button>();
            _scoreModel = scoreModel;

            _button.enabled = false;
        }

        private void Start()
        {
            _button
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    var tweetText = $"{_scoreModel.GetScore()}個増やすことができた！\n";
                    tweetText += $"#{_hashTag1} #{_hashTag2}\n";
                    UnityRoomTweet.Tweet(_gameId, tweetText);
                })
                .AddTo(this);
        }

        public void PlayFadeIn()
        {
            _button.FadeInButton();

            var position = new Vector3(-0.85f, 1.4f, 0f);
            Instantiate(effect, position, Quaternion.identity);
        }
    }
}