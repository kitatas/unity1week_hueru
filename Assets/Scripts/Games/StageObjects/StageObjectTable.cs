using UnityEngine;

namespace Games.StageObjects
{
    /// <summary>
    /// ふやすオブジェクトのScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "StageObjectTable", menuName = "DataTable/StageObjectTable", order = 0)]
    public sealed class StageObjectTable : ScriptableObject
    {
        [SerializeField] private GameObject circle = null;
        [SerializeField] private GameObject triangle = null;
        [SerializeField] private GameObject square = null;

        /// <summary>
        /// ふやすオブジェクトの配列取得
        /// </summary>
        public GameObject[] StageObjectList => new GameObject[]
        {
            circle,
            triangle,
            square,
        };
    }
}