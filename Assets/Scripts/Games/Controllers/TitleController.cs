using System.Threading;
using Configs;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Games.Sounds;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Games.Controllers
{
    public sealed class TitleController : MonoBehaviour
    {
        [SerializeField] private RectTransform[] titleStrings = null;

        private readonly float _z = 10f;

        [Inject]
        private void Construct(BgmController bgmController)
        {
            bgmController.PlayBgm(BgmType.Main);

            var token = this.GetCancellationTokenOnDestroy();
            foreach (var titleString in titleStrings)
            {
                var z = Random.Range(-_z, _z);
                titleString.localRotation = Quaternion.Euler(new Vector3(0f, 0f, z));

                TweenStringAsync(titleString, token).Forget();
            }
        }

        private async UniTaskVoid TweenStringAsync(RectTransform titleString, CancellationToken token)
        {
            var animationTime = Random.Range(0.5f, 1f);
            await titleString
                .DOLocalRotate(new Vector3(0f, 0f, _z), animationTime)
                .WithCancellation(token);

            var sequence = DOTween.Sequence()
                .Append(titleString
                    .DOLocalRotate(new Vector3(0f, 0f, -_z), animationTime))
                .Append(titleString
                    .DOLocalRotate(new Vector3(0f, 0f, _z), animationTime))
                .SetLoops(-1, LoopType.Yoyo);

            this.OnDisableAsObservable()
                .Subscribe(_ => sequence?.Kill())
                .AddTo(this);
        }
    }
}