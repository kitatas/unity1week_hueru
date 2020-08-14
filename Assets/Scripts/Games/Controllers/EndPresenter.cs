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

            _tweetButton.PlayEffect();
            _rankingButton.PlayEffect();
        }

        private void FadeInText()
        {
            var offsetAddValue = new Vector3(0f, 30f, 0f);
            var tmpAnimator = new DOTweenTMPAnimator(gameOverText);
            gameOverText
                .DOFade(0f, 0f);

            for (int i = 0; i < tmpAnimator.textInfo.characterCount; i++)
            {
                tmpAnimator.DOScaleChar(i, 0.7f, 0f);
                var offset = tmpAnimator.GetCharOffset(i);
                DOTween.Sequence()
                    .Append(tmpAnimator
                        .DOOffsetChar(i, offset + offsetAddValue, 0.4f)
                        .SetEase(Ease.OutFlash, 2f))
                    .Join(tmpAnimator
                        .DOFadeChar(i, _fadeInValue, 0.4f))
                    .Join(tmpAnimator
                        .DOScaleChar(i, _fadeInValue, 0.4f)
                        .SetEase(Ease.OutBack))
                    .SetDelay(0.07f * i);
            }
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