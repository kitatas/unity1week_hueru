using System;
using Configs;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Games.Buttons
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(ButtonSpeaker))]
    public sealed class PopupButton : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup = null;
        [SerializeField] private PopType popType = default;

        private readonly float _animationTime = 0.25f;

        private void Start()
        {
            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ => TweenWindow())
                .AddTo(this);
        }

        private void TweenWindow()
        {
            switch (popType)
            {
                case PopType.Open:
                    Open();
                    break;
                case PopType.Close:
                    Close();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Open()
        {
            canvasGroup.blocksRaycasts = true;

            DOTween.Sequence()
                .Append(DOTween
                    .To(() => canvasGroup.alpha,
                        alpha => canvasGroup.alpha = alpha,
                        1f,
                        _animationTime)
                    .SetEase(Ease.OutBack))
                .Join((canvasGroup.transform as RectTransform)
                    .DOScale(Vector3.one, _animationTime)
                    .SetEase(Ease.OutBack));
        }

        private void Close()
        {
            canvasGroup.blocksRaycasts = false;

            DOTween.Sequence()
                .Append(DOTween
                    .To(() => canvasGroup.alpha,
                        alpha => canvasGroup.alpha = alpha,
                        0f,
                        _animationTime)
                    .SetEase(Ease.OutQuart))
                .Join((canvasGroup.transform as RectTransform)
                    .DOScale(Vector3.one * 0.8f, _animationTime)
                    .SetEase(Ease.OutQuart));
        }
    }
}