using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
    /// <summary>
    /// ボタンの拡張メソッドを管理するクラス
    /// </summary>
    public static class GraphicExtensions
    {
        private static readonly float _animationTime = 0.25f;

        /// <summary>
        /// フェードイン
        /// </summary>
        /// <param name="graphic"></param>
        /// <param name="completeAction"></param>
        public static void FadeInButton(this Graphic graphic, Action completeAction = null)
        {
            DOTween.Sequence()
                .Append(graphic
                    .DOFade(1.0f, _animationTime)
                    .SetEase(Ease.Linear))
                .Join(graphic.rectTransform
                    .DOScale(Vector3.one, _animationTime)
                    .SetEase(Ease.OutBack))
                .OnComplete(() => completeAction?.Invoke());
        }
    }
}