using DG.Tweening;
using UnityEngine;

namespace Games.StageObjects
{
    public sealed class StageObject : MonoBehaviour
    {
        private readonly float _animationTime = 0.5f;

        private void Start()
        {
            transform
                .DOScale(transform.localScale * 2f, _animationTime);
        }
    }
}