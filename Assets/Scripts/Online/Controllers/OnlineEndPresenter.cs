using System;
using Configs;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Utility;
using Zenject;

namespace Online.Controllers
{
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

        public void Play(FinishType finishType)
        {
            _mainCamera
                .DOShakePosition(_shakeTime, _shakeStrength);

            gameFinishText.text = GetFinishText(finishType);
            gameFinishText.colorGradient = GetTextColorGradient(finishType);
            gameFinishText.FadeInText();
        }

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