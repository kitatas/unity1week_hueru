using DG.Tweening;
using Games.Buttons;
using TMPro;
using UnityEngine;
using Utility;
using Zenject;

namespace Games.Controllers
{
    /// <summary>
    /// ゲーム終了演出を行うクラス
    /// </summary>
    public sealed class EndPresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI gameOverText = null;
        [SerializeField] private ButtonFader tweetButton = null;
        [SerializeField] private ButtonFader rankingButton = null;

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
        public void Play()
        {
            _mainCamera
                .DOShakePosition(_shakeTime, _shakeStrength);

            gameOverText.FadeInText();
            tweetButton.PlayFadeIn();
            rankingButton.PlayFadeIn();
        }
    }
}