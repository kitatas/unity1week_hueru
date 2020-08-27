using System;
using Configs;
using Games.Controllers;
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

        private CameraController _cameraController;

        [Inject]
        private void Construct(CameraController cameraController)
        {
            _cameraController = cameraController;
        }

        /// <summary>
        /// ゲーム終了演出の再生
        /// </summary>
        /// <param name="finishType"></param>
        public void Play(FinishType finishType)
        {
            _cameraController.Shake();

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