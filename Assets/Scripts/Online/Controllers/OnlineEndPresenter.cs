using System;
using Configs;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Utility;
using Zenject;

namespace Online.Controllers
{
    /// <summary>
    /// PUN版
    /// ゲーム終了演出を行うクラス
    /// </summary>
    public sealed class OnlineEndPresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI gameFinishText = null;

        private readonly float _shakeTime = 0.75f;
        private readonly float _shakeStrength = 0.75f;

        private Camera _mainCamera;

        [Inject]
        private void Construct(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }

        /// <summary>
        /// ゲーム終了演出の再生
        /// </summary>
        /// <param name="finishType"></param>
        public void Play(FinishType finishType)
        {
            _mainCamera
                .DOShakePosition(_shakeTime, _shakeStrength);

            gameFinishText.text = GetFinishText(finishType);
            gameFinishText.colorGradient = GetTextColorGradient(finishType);
            gameFinishText.FadeInText();
        }

        /// <summary>
        /// 勝敗の文言取得
        /// </summary>
        /// <param name="finishType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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

        /// <summary>
        /// 勝敗文言の色取得
        /// </summary>
        /// <param name="finishType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private static VertexGradient GetTextColorGradient(FinishType finishType)
        {
            Color topColor;
            Color bottomColor;
            switch (finishType)
            {
                case FinishType.Win:
                    topColor = Color.red;
                    bottomColor = new Color(1.0f, 0.9f, 0.9f);
                    break;
                case FinishType.Lose:
                    topColor = Color.blue;
                    bottomColor = new Color(0.9f, 0.9f, 1.0f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(finishType), finishType, null);
            }

            return new VertexGradient(topColor, topColor, bottomColor, bottomColor);
        }
    }
}