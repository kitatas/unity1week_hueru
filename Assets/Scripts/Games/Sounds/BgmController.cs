using Configs;
using UnityEngine;
using Utility;
using Zenject;

namespace Games.Sounds
{
    public sealed class BgmController : BaseAudioSource
    {
        private AudioClip[] _bgmList;

        [Inject]
        private void Construct(BgmTable bgmTable)
        {
            _bgmList = bgmTable.GetBgmList();
        }

        public void PlayBgm(BgmType bgmType)
        {
            if (_bgmList.TryGetValue((int) bgmType, out var clip))
            {
                AudioSource.clip = clip;
                AudioSource.Play();
            }
            else
            {
                Debug.LogError(nameof(bgmType) + "is not contain BgmTable.");
            }
        }

        public void StopBgm()
        {
            AudioSource.Stop();
        }
    }
}