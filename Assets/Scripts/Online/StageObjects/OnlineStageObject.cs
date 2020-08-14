using Configs;
using DG.Tweening;
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

        private OnlineGameController _onlineGameController;

        [Inject]
        private void Construct(OnlineGameController onlineGameController)
        {
            _onlineGameController = onlineGameController;
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
                    _onlineGameController.SetGameOver();
                })
                .AddTo(this);
        }
    }
}