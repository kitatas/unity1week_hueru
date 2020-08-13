using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Games.Controllers
{
    public sealed class StartPresenter : MonoBehaviour
    {
        [SerializeField] private Transform gameStage = null;
        [SerializeField] private Transform gameOverArea = null;

        private readonly float _stageHeight = 0f;
        private readonly float _hitHeight = 2.56f;
        private readonly float _bottomHeight = -5.688f;
        private readonly float _animationTime = 0.5f;

        public void Play(Action action)
        {
            var token = this.GetCancellationTokenOnDestroy();
            PlayAsync(token, action).Forget();
        }

        private async UniTaskVoid PlayAsync(CancellationToken token, Action action)
        {
            await gameStage
                .DOLocalMoveY(_hitHeight, _animationTime)
                .SetEase(Ease.Linear)
                .WithCancellation(token);

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