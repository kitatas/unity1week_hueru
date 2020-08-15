using Configs;
using DG.Tweening;
using Games.Sounds;
using Online.Controllers;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Online.StageObjects
{
    public sealed class OnlineStageObject : MonoBehaviour
    {
        [SerializeField] private GameObject effect = null;

        private readonly float _animationTime = 0.5f;

        private SeController _seController;
        private OnlineGameController _onlineGameController;

        [Inject]
        private void Construct(SeController seController, OnlineGameController onlineGameController)
        {
            _seController = seController;
            _onlineGameController = onlineGameController;
        }

        private void Start()
        {
            _seController.PlaySe(SeType.Generate);

            transform
                .DOScale(transform.localScale * 2f, _animationTime);

            this.OnTriggerEnter2DAsObservable()
                .Where(other => other.CompareTag(Tag.GAME_OVER_AREA))
                .Subscribe(_ =>
                {
                    _seController.PlaySe(SeType.Explode);
                    Instantiate(effect, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    _onlineGameController.SetGameOver();
                })
                .AddTo(this);
        }
    }
}