using Configs;
using UnityEngine;

namespace Games.Sounds
{
    [CreateAssetMenu(fileName = "BgmTable", menuName = "DataTable/BgmTable", order = 0)]
    public sealed class BgmTable : ScriptableObject
    {
        [SerializeField] private AudioClip title = null;
        [SerializeField] private AudioClip main = null;

        public AudioClip[] GetBgmList()
        {
            var bgmCount = System.Enum.GetValues(typeof(BgmType)).Length;
            var bgmList = new AudioClip[bgmCount];
            bgmList[(int) BgmType.Title] = title;
            bgmList[(int) BgmType.Main] = main;

            return bgmList;
        }
    }
}