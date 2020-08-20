namespace Games.Sounds
{
    /// <summary>
    /// 音量制御を扱うinterface
    /// </summary>
    public interface IVolumeUpdatable
    {
        /// <summary>
        /// 音量の取得
        /// </summary>
        /// <returns></returns>
        float GetVolume();

        /// <summary>
        /// 音量の代入
        /// </summary>
        /// <param name="value"></param>
        void SetVolume(float value);
    }
}