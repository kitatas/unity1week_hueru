using System;
using Configs;
using Games.Sounds;
using Games.StageObjects;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Games.Controllers
{
    public sealed class GameController : MonoBehaviour
    {
        [SerializeField] private bool isFinishDebug = false;

        private bool _isStart;
        private ReactiveProperty<bool> _isGameOver;

        private StageObjectRepository _stageObjectRepository;

        [Inject]
        private void Construct(StartPresenter startPresenter, EndPresenter endPresenter,
            StageObjectRepository stageObjectRepository, SeController seController)
        {
            _stageObjectRepository = stageObjectRepository;

            _isStart = false;
            _isGameOver = new ReactiveProperty<bool>(false);

            startPresenter.Play(() =>
            {
                _isStart = true;
                var position = isFinishDebug ? new Vector3(2.5f, 1.5f) : Vector3.zero;
                GenerateStageObject(position);
            });

            _isGameOver
                .Where(x => x)
                .Subscribe(_ =>
                {
                    endPresenter.Play();
                    seController.PlaySe(SeType.Finish);
                })
                .AddTo(this);
        }

        public IObservable<Unit> PlayingAsObservable =>
            this.UpdateAsObservable()
                .Where(_ => _isStart && _isGameOver.Value == false);

        public void SetGameOver()
        {
            _isGameOver.Value = true;
        }

        public void GenerateStageObject(Vector3 generatePosition)
        {
            _stageObjectRepository.GenerateStageObject(generatePosition);
        }
    }
}