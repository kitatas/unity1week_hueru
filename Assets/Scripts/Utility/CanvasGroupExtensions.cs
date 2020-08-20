using DG.Tweening;
using UnityEngine;

namespace Utility
{
    /// <summary>
    /// CanvasGroupの拡張メソッドを管理するうクラス
    /// </summary>
    public static class CanvasGroupExtensions
    {
        private static readonly float _animationTime = 0.25f;

        /// <summary>
        /// フェードイン
        /// </summary>
        /// <param name="canvasGroup"></param>
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

        /// <summary>
        /// フェードアウト
        /// </summary>
        /// <param name="canvasGroup"></param>
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