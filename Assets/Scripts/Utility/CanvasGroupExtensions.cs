using DG.Tweening;
using UnityEngine;

namespace Utility
{
    public static class CanvasGroupExtensions
    {
        private static readonly float _animationTime = 0.25f;

        public static void PopUpOpen(this CanvasGroup canvasGroup)
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

        public static void PopUpClose(this CanvasGroup canvasGroup)
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