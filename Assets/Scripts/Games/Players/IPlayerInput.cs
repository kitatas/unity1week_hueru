using UnityEngine;

namespace Games.Players
{
    public interface IPlayerInput
    {
        bool InputMouseButton { get; }
        Vector3 MousePosition { get; }
    }
}