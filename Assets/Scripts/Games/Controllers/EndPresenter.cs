using DG.Tweening;
using Games.Buttons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Games.Controllers
{
    public sealed class EndPresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI gameOverText = null;

        private readonly float _shakeTime = 0.75f;
        private readonly float _shakeStrength = 0.75f;
        private readonly float _animationTime = 0.5f;
        private readonly float _fadeInValue = 1.0f;

        private Camera _mainCamera;
        private TweetButton _tweetButton;
        private RankingButton _rankingButton;

        [Inject]
        private void Construct(Camera mainCamera, TweetButton tweetButton, RankingButton rankingButton)
        {
            _mainCamera = mainCamera;
            _tweetButton = tweetButton;
            _rankingButton = rankingButton;
        }

        public void Play()
        {
            _mainCamera
                .DOShakePosition(_shakeTime, _shakeStrength);

            FadeInText();
            FadeInButton(_tweetButton.Button);
            FadeInButton(_rankingButton.Button);
        }

        private void FadeInText()
        {
            gameOverText
                .DOFade(_fadeInValue, _animationTime)
                .SetEase(Ease.Linear);

            gameOverText.rectTransform
                .DOScale(Vector3.one, _animationTime)
                .SetEase(Ease.OutBack);
        }

        private void FadeInButton(Button button)
        {
            button.image
                .DOFade(_fadeInValue, _animationTime)
                .SetEase(Ease.Linear)
                .OnComplete(() => button.enabled = true);

            button.image.rectTransform
                .DOScale(Vector3.one, _animationTime)
                .SetEase(Ease.OutBack);
        }
    }
}