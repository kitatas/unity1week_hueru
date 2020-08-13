using Configs;
using DG.Tweening;
using Games.Controllers;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Games.StageObjects
{
    public sealed class StageObject : MonoBehaviour
    {
        [SerializeField] private GameObject effect = null;

        private readonly float _animationTime = 0.5f;

        private GameController _gameController;

        [Inject]
        private void Construct(GameController gameController)
        {
            _gameController = gameController;
        }

        private void Start()
        {
            transform
                .DOScale(transform.localScale * 2f, _animationTime);

            this.OnTriggerEnter2DAsObservable()
                .Where(other => other.CompareTag(Tag.GAME_OVER_AREA))
                .Subscribe(_ =>
                {
                    Instantiate(effect, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    _gameController.SetGameOver();
                })
                .AddTo(this);
        }
    }
}