using UnityEngine;

namespace Games.StageObjects
{
    [CreateAssetMenu(fileName = "StageObjectTable", menuName = "DataTable/StageObjectTable", order = 0)]
    public sealed class StageObjectTable : ScriptableObject
    {
        [SerializeField] private GameObject circle = null;
        [SerializeField] private GameObject triangle = null;
        [SerializeField] private GameObject square = null;

        private GameObject[] StageObjectList => new GameObject[]
        {
            circle,
            triangle,
            square,
        };

        public GameObject GetStageObject()
        {
            return StageObjectList[Random.Range(0, StageObjectList.Length)];
        }
    }
}