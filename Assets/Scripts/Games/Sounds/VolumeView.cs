using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Games.Sounds
{
    /// <summary>
    /// 音量調整Slider制御のクラス
    /// </summary>
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

        /// <summary>
        /// BGMの音量とSliderの値の同期
        /// </summary>
        /// <param name="volumeUpdatable"></param>
        private void UpdateBgmVolume(IVolumeUpdatable volumeUpdatable)
        {
            bgmSlider.value = volumeUpdatable.GetVolume();
            bgmSlider
                .OnValueChangedAsObservable()
                .Subscribe(volumeUpdatable.SetVolume)
                .AddTo(this);
        }

        /// <summary>
        /// SEの音量とSliderの値の同期
        /// </summary>
        /// <param name="volumeUpdatable"></param>
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