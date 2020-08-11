using Games.Players;
using UnityEngine;

namespace Configs
{
    public sealed class InputProvider : IPlayerInput
    {
        public bool InputMouseButton => Input.GetMouseButtonDown(0);

        public Vector3 MousePosition => Input.mousePosition;
    }
}