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
    /// <summary>
    /// ゲームの進行を管理するクラス
    /// </summary>
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

            // ゲーム開始演出の実行
            startPresenter.Play(() =>
            {
                _isStart = true;
                var position = isFinishDebug ? new Vector3(2.5f, 1.5f) : Vector3.zero;
                GenerateStageObject(position);
            });

            // ゲームオーバー演出の実行
            _isGameOver
                .Where(x => x)
                .Subscribe(_ =>
                {
                    endPresenter.Play();
                    seController.PlaySe(SeType.Finish);
                })
                .AddTo(this);
        }

        /// <summary>
        /// Play可能時のUpdateAsObservable
        /// </summary>
        public IObservable<Unit> PlayingAsObservable =>
            this.UpdateAsObservable()
                .Where(_ => _isStart && _isGameOver.Value == false);

        /// <summary>
        /// ゲーム終了に
        /// </summary>
        public void SetGameOver()
        {
            _isGameOver.Value = true;
        }

        /// <summary>
        /// StageObjectの生成
        /// </summary>
        /// <param name="generatePosition"></param>
        public void GenerateStageObject(Vector3 generatePosition)
        {
            _stageObjectRepository.GenerateStageObject(generatePosition);
        }
    }
}