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

        private CameraController _cameraController;

        [Inject]
        private void Construct(CameraController cameraController)
        {
            _cameraController = cameraController;
        }

        /// <summary>
        /// ゲーム終了演出の再生
        /// </summary>
        public void Play()
        {
            _cameraController.Shake();

            gameOverText.FadeInText();
            tweetButton.PlayFadeIn();
            rankingButton.PlayFadeIn();
        }
    }
}