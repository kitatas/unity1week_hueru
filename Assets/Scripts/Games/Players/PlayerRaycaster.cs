using UnityEngine;

namespace Games.Players
{
    public sealed class PlayerRaycaster
    {
        private readonly Camera _mainCamera;

        public PlayerRaycaster(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputPosition"></param>
        /// <returns></returns>
        public GameObject GetClickObject(Vector3 inputPosition)
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            var hit = Physics2D.Raycast(ray.origin, ray.direction);
            return hit == false ? null : hit.collider.gameObject;
        }
    }
}