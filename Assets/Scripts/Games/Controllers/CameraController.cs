using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Games.Controllers
{
    /// <summary>
    /// カメラの制御クラス
    /// </summary>
    public sealed class CameraController
    {
        private readonly float _shakeTime = 0.75f;
        private readonly float _shakeStrength = 0.75f;

        private Camera _mainCamera;

        [Inject]
        private void Construct(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }

        public Camera MainCamera => _mainCamera;

        public void Shake()
        {
            _mainCamera
                .DOShakePosition(_shakeTime, _shakeStrength);
        }
    }
}