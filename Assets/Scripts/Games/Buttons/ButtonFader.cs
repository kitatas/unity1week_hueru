using Games.Controllers;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using Zenject;

namespace Games.Buttons
{
    /// <summary>
    /// ボタンのフェードインの演出
    /// </summary>
    public sealed class ButtonFader : MonoBehaviour
    {
        [SerializeField] private GameObject effect = null;

        private Button _button;
        private Camera _mainCamera;

        [Inject]
        private void Construct(CameraController cameraController)
        {
            _button = GetComponent<Button>();
            _mainCamera = cameraController.MainCamera;

            _button.enabled = false;
        }

        /// <summary>
        /// ボタン表示時の演出
        /// </summary>
        public void PlayFadeIn()
        {
            _button.FadeInButton();

            var position = _button.image.rectTransform.GetWorldPosition(_mainCamera);
            Instantiate(effect, position, Quaternion.identity);
        }
    }
}