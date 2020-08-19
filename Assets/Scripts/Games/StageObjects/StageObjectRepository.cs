using UnityEngine;
using Zenject;

namespace Games.StageObjects
{
    public sealed class StageObjectRepository : MonoBehaviour
    {
        private readonly float _radius = 0.125f;

        private GameObject[] _stageObjectList;

        [Inject]
        private void Construct(StageObjectTable stageObjectTable)
        {
            _stageObjectList = stageObjectTable.StageObjectList;
        }

        public void GenerateStageObject(Vector3 clickObjectPosition)
        {
            var stageObject = Instantiate(GetStageObject(), transform);
            stageObject.transform.position = GetGeneratePosition(clickObjectPosition);
        }

        public string GetGenerateStageObjectName()
        {
            return GetStageObject().name;
        }

        private GameObject GetStageObject()
        {
            return _stageObjectList[Random.Range(0, _stageObjectList.Length)];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clickObjectPosition"></param>
        /// <returns></returns>
        public Vector3 GetGeneratePosition(Vector3 clickObjectPosition)
        {
            var random = Random.Range(0f, 360f);
            var theta = random * Mathf.PI / 180f;
            var x = Mathf.Cos(theta) * _radius + clickObjectPosition.x;
            var y = Mathf.Sin(theta) * _radius + clickObjectPosition.y;
            return new Vector3(x, y, 0f);
        }
    }
}