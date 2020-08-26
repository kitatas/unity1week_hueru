using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
    /// <summary>
    /// ボタンの拡張メソッドを管理するクラス
    /// </summary>
    public static class ButtonExtensions
    {
        private static readonly float _animationTime = 0.5f;

        /// <summary>
        /// フェードイン
        /// </summary>
        /// <param name="button"></param>
        public static void FadeInButton(this Button button)
        {
            button.image
                .DOFade(1.0f, _animationTime)
                .SetEase(Ease.Linear)
                .OnComplete(() => button.enabled = true);

            button.image.rectTransform
                .DOScale(Vector3.one, _animationTime)
                .SetEase(Ease.OutBack);
        }
    }
}