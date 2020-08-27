using Games.Score;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Games.Buttons
{
    /// <summary>
    /// TweetするWindowを開くボタン
    /// </summary>
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(ButtonFader))]
    [RequireComponent(typeof(ButtonSpeaker))]
    public sealed class TweetButton : MonoBehaviour
    {
        private const string GAME_ID = "huyasu_huyasu";
        private const string HASH_TAG1 = "unityroom";
        private const string HASH_TAG2 = "unity1week";

        private IScoreModel _scoreModel;

        [Inject]
        private void Construct(IScoreModel scoreModel)
        {
            _scoreModel = scoreModel;
        }

        private void Start()
        {
            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    var tweetText = $"{_scoreModel.GetScore()}個増やすことができた！\n";
                    tweetText += $"#{HASH_TAG1} #{HASH_TAG2}\n";
                    UnityRoomTweet.Tweet(GAME_ID, tweetText);
                })
                .AddTo(this);
        }
    }
}