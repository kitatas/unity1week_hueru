using Games.Controllers;
using UnityEngine;

namespace Games.Players
{
    /// <summary>
    /// Playerの操作によるRayを管理するクラス
    /// </summary>
    public sealed class PlayerRaycaster
    {
        private readonly Camera _mainCamera;

        public PlayerRaycaster(CameraController cameraController)
        {
            _mainCamera = cameraController.MainCamera;
        }

        /// <summary>
        /// マウス位置からGameObjectの取得
        /// </summary>
        /// <param name="inputPosition"></param>
        /// <returns></returns>
        public GameObject GetClickObject(Vector3 inputPosition)
        {
            var ray = _mainCamera.ScreenPointToRay(inputPosition);
            var hit = Physics2D.Raycast(ray.origin, ray.direction);
            return hit == false ? null : hit.collider.gameObject;
        }
    }
}