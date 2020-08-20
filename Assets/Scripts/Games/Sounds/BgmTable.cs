using Configs;
using UnityEngine;

namespace Games.Sounds
{
    /// <summary>
    /// BGMのScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "BgmTable", menuName = "DataTable/BgmTable", order = 0)]
    public sealed class BgmTable : ScriptableObject
    {
        [SerializeField] private AudioClip main = null;

        /// <summary>
        /// 音ファイルとBgmTypeの紐付け
        /// </summary>
        /// <returns></returns>
        public AudioClip[] GetBgmList()
        {
            var bgmCount = System.Enum.GetValues(typeof(BgmType)).Length;
            var bgmList = new AudioClip[bgmCount];
            bgmList[(int) BgmType.Main] = main;

            return bgmList;
        }
    }
}