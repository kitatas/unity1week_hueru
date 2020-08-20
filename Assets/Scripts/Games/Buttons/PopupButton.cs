using System;
using Configs;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Games.Buttons
{
    /// <summary>
    /// UIのポップアップを行うボタン
    /// </summary>
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(ButtonSpeaker))]
    public sealed class PopupButton : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup = null;
        [SerializeField] private PopType popType = default;

        private void Start()
        {
            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ => TweenWindow())
                .AddTo(this);
        }

        /// <summary>
        /// PopTypeから該当するCanvasのアニメーションを行う。
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void TweenWindow()
        {
            switch (popType)
            {
                case PopType.Open:
                    canvasGroup.PopUpOpen();
                    break;
                case PopType.Close:
                    canvasGroup.PopUpClose();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}