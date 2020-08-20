using UnityEngine;

namespace Games.Sounds
{
    /// <summary>
    /// 音制御の抽象クラス
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public abstract class BaseAudioSource : MonoBehaviour, IVolumeUpdatable
    {
        private AudioSource _audioSource;

        protected AudioSource AudioSource
        {
            get
            {
                if (_audioSource == null)
                {
                    _audioSource = GetComponent<AudioSource>();
                }

                return _audioSource;
            }
        }

        /// <summary>
        /// 音量の取得
        /// </summary>
        /// <returns></returns>
        public float GetVolume() => AudioSource.volume;

        /// <summary>
        /// 音量の代入
        /// </summary>
        /// <param name="value"></param>
        public void SetVolume(float value) => AudioSource.volume = value;
    }
}