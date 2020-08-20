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
    /// <summary>
    /// PUN用
    /// ふやすオブジェクト制御クラス
    /// </summary>
    public sealed class OnlineStageObject : MonoBehaviour
    {
        [SerializeField] private GameObject generateEffect = null;
        [SerializeField] private GameObject explodeEffect = null;

        private readonly float _animationTime = 0.5f;

        private SeController _seController;
        private OnlineGameController _onlineGameController;

        [Inject]
        private void Construct(SeController seController, OnlineGameController onlineGameController,
            StageObjectContainer stageObjectContainer)
        {
            _seController = seController;
            _onlineGameController = onlineGameController;

            stageObjectContainer.AddStageObject(this);
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
                    _onlineGameController.SetGameOver();
                })
                .AddTo(this);
        }
    }
}