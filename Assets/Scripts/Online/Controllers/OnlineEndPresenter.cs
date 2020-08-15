using System;
using Configs;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Online.Controllers
{
    public sealed class OnlineEndPresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI gameFinishText = null;

        private readonly float _shakeTime = 0.75f;
        private readonly float _shakeStrength = 0.75f;
        private readonly float _fadeInValue = 1.0f;

        private Camera _mainCamera;

        [Inject]
        private void Construct(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }

        public void Play(FinishType finishType)
        {
            _mainCamera
                .DOShakePosition(_shakeTime, _shakeStrength);

            gameFinishText.text = GetFinishText(finishType);
            FadeInText();
        }

        private static string GetFinishText(FinishType finishType)
        {
            switch (finishType)
            {
                case FinishType.Win:
                    return "あなたの勝ち";
                case FinishType.Lose:
                    return "あなたの負け";
                default:
                    throw new ArgumentOutOfRangeException(nameof(finishType), finishType, null);
            }
        }

        private void FadeInText()
        {
            var offsetAddValue = new Vector3(0f, 30f, 0f);
            var tmpAnimator = new DOTweenTMPAnimator(gameFinishText);
            gameFinishText
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
    }
}