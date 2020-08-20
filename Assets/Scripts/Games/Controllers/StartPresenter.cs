using System;
using System.Threading;
using Configs;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Games.Sounds;
using UnityEngine;
using Zenject;

namespace Games.Controllers
{
    /// <summary>
    /// ゲーム開始演出を行うクラス
    /// </summary>
    public sealed class StartPresenter : MonoBehaviour
    {
        [SerializeField] private Transform gameStage = null;
        [SerializeField] private Transform gameOverArea = null;

        private readonly float _stageHeight = 0f;
        private readonly float _hitHeight = 2.56f;
        private readonly float _bottomHeight = -5.688f;
        private readonly float _animationTime = 0.5f;

        private SeController _seController;

        [Inject]
        private void Construct(SeController seController)
        {
            _seController = seController;
        }

        /// <summary>
        /// ゲーム開始演出の再生
        /// </summary>
        /// <param name="action"></param>
        public void Play(Action action)
        {
            var token = this.GetCancellationTokenOnDestroy();
            PlayAsync(token, action).Forget();
        }

        /// <summary>
        /// ステージ出現のアニメーション
        /// </summary>
        /// <param name="token"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private async UniTaskVoid PlayAsync(CancellationToken token, Action action)
        {
            await gameStage
                .DOLocalMoveY(_hitHeight, _animationTime)
                .SetEase(Ease.Linear)
                .WithCancellation(token);

            _seController.PlaySe(SeType.Appear);

            await (
                gameStage
                    .DOLocalMoveY(_stageHeight, _animationTime)
                    .SetEase(Ease.OutBack)
                    .WithCancellation(token),
                gameOverArea
                    .DOLocalMoveY(_bottomHeight, _animationTime)
                    .SetEase(Ease.OutBack)
                    .WithCancellation(token)
            );

            action();
        }
    }
}