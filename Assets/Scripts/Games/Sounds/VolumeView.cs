using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Games.Sounds
{
    public sealed class VolumeView : MonoBehaviour
    {
        [SerializeField] private Slider bgmSlider = null;
        [SerializeField] private Slider seSlider = null;

        [Inject]
        private void Construct(BgmController bgmController, SeController seController)
        {
            UpdateBgmVolume(bgmController);
            UpdateSeVolume(seController);
        }

        private void UpdateBgmVolume(IVolumeUpdatable volumeUpdatable)
        {
            bgmSlider.value = volumeUpdatable.GetVolume();
            bgmSlider
                .OnValueChangedAsObservable()
                .Subscribe(volumeUpdatable.SetVolume)
                .AddTo(this);
        }

        private void UpdateSeVolume(IVolumeUpdatable volumeUpdatable)
        {
            seSlider.value = volumeUpdatable.GetVolume();
            seSlider
                .OnValueChangedAsObservable()
                .Subscribe(volumeUpdatable.SetVolume)
                .AddTo(this);
        }
    }
}