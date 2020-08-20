using Games.Score;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using Zenject;

namespace Games.Buttons
{
    /// <summary>
    /// TweetするWindowを開くボタン
    /// </summary>
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(ButtonSpeaker))]
    public sealed class TweetButton : MonoBehaviour
    {
        [SerializeField] private GameObject effect = null;

        private const string GAME_ID = "huyasu_huyasu";
        private const string HASH_TAG1 = "unityroom";
        private const string HASH_TAG2 = "unity1week";

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
                    tweetText += $"#{HASH_TAG1} #{HASH_TAG2}\n";
                    UnityRoomTweet.Tweet(GAME_ID, tweetText);
                })
                .AddTo(this);
        }

        /// <summary>
        /// ボタン表示時の演出
        /// </summary>
        public void PlayFadeIn()
        {
            _button.FadeInButton();

            var position = new Vector3(-0.85f, 1.4f, 0f);
            Instantiate(effect, position, Quaternion.identity);
        }
    }
}