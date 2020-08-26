using UnityEngine;

namespace Utility
{
    /// <summary>
    /// RectTransformの拡張メソッドを管理するクラス
    /// </summary>
    public static class RectTransformExtensions
    {
        /// <summary>
        /// UI座標からワールド座標に変換
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="mainCamera"></param>
        /// <returns></returns>
        public static Vector3 GetWorldPosition(this RectTransform rect, Camera mainCamera)
        {
            var screenPos = RectTransformUtility.WorldToScreenPoint(mainCamera, rect.position);

            RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, screenPos, mainCamera, out var result);

            return result;
        }
    }
}