using UnityEngine;

namespace Games.Players
{
    /// <summary>
    /// Playerの入力を扱うinterface
    /// </summary>
    public interface IPlayerInput
    {
        /// <summary>
        /// マウスクリック
        /// </summary>
        bool InputMouseButton { get; }

        /// <summary>
        /// マウスクリックの位置
        /// </summary>
        Vector3 MousePosition { get; }
    }
}