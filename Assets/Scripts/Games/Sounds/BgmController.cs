using Configs;
using UnityEngine;
using Utility;
using Zenject;

namespace Games.Sounds
{
    /// <summary>
    /// BGMの制御クラス
    /// </summary>
    public sealed class BgmController : BaseAudioSource
    {
        private AudioClip[] _bgmList;

        [Inject]
        private void Construct(BgmTable bgmTable)
        {
            _bgmList = bgmTable.GetBgmList();
        }

        /// <summary>
        /// BGMの再生
        /// </summary>
        /// <param name="bgmType"></param>
        public void PlayBgm(BgmType bgmType)
        {
            if (_bgmList.TryGetValue((int) bgmType, out var clip))
            {
                if (AudioSource.clip == clip)
                {
                    return;
                }

                AudioSource.clip = clip;
                AudioSource.Play();
            }
            else
            {
                Debug.LogError(nameof(bgmType) + "is not contain BgmTable.");
            }
        }

        /// <summary>
        ///  BGMの停止
        /// </summary>
        public void StopBgm()
        {
            AudioSource.Stop();
        }
    }
}