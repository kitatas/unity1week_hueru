using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
    public static class ButtonExtensions
    {
        private static readonly float _animationTime = 0.5f;

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