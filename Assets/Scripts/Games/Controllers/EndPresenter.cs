using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Games.Controllers
{
    public sealed class EndPresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI gameOverText = null;

        private readonly float _shakeTime = 0.75f;
        private readonly float _shakeStrength = 0.75f;
        private readonly float _aniamtionTime = 0.5f;

        private Camera _mainCamera;

        [Inject]
        private void Construct(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }

        public void Play()
        {
            _mainCamera
                .DOShakePosition(_shakeTime, _shakeStrength);

            gameOverText.rectTransform
                .DOAnchorPosY(-gameOverText.rectTransform.anchoredPosition.y, _aniamtionTime)
                .SetEase(Ease.Linear);
        }
    }
}