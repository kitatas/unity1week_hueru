using Configs;
using DG.Tweening;
using Games.Controllers;
using Games.Sounds;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Games.StageObjects
{
    public sealed class StageObject : MonoBehaviour
    {
        [SerializeField] private GameObject generateEffect = null;
        [SerializeField] private GameObject explodeEffect = null;

        private readonly float _animationTime = 0.5f;

        private SeController _seController;
        private GameController _gameController;

        [Inject]
        private void Construct(SeController seController, GameController gameController)
        {
            _seController = seController;
            _gameController = gameController;
        }

        private void Start()
        {
            _seController.PlaySe(SeType.Generate);
            Instantiate(generateEffect, transform.position, Quaternion.identity);

            transform
                .DOScale(transform.localScale * 2f, _animationTime);

            this.OnTriggerEnter2DAsObservable()
                .Where(other => other.CompareTag(Tag.GAME_OVER_AREA))
                .Subscribe(_ =>
                {
                    _seController.PlaySe(SeType.Explode);
                    Instantiate(explodeEffect, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    _gameController.SetGameOver();
                })
                .AddTo(this);
        }
    }
}