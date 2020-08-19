using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Utility
{
    public static class TextMeshProExtensions
    {
        public static void FadeInText(this TextMeshProUGUI textMeshProUGUI)
        {
            var offsetAddValue = new Vector3(0f, 30f, 0f);
            var tmpAnimator = new DOTweenTMPAnimator(textMeshProUGUI);
            textMeshProUGUI
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
                        .DOFadeChar(i, 1.0f, 0.4f))
                    .Join(tmpAnimator
                        .DOScaleChar(i, 1.0f, 0.4f)
                        .SetEase(Ease.OutBack))
                    .SetDelay(0.07f * i);
            }
        }
    }
}