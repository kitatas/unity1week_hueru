using UnityEngine;
using Zenject;

namespace Games.StageObjects
{
    public sealed class StageObjectRepository : MonoBehaviour
    {
        private readonly float _radius = 0.125f;

        private StageObjectTable _stageObjectTable;

        [Inject]
        private void Construct(StageObjectTable stageObjectTable)
        {
            _stageObjectTable = stageObjectTable;
        }

        public void GenerateStageObject(Vector3 clickObjectPosition)
        {
            var stageObject = Instantiate(_stageObjectTable.GetStageObject(), transform);
            stageObject.transform.position = GetGeneratePosition(clickObjectPosition);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clickObjectPosition"></param>
        /// <returns></returns>
        private Vector3 GetGeneratePosition(Vector3 clickObjectPosition)
        {
            var random = Random.Range(0f, 360f);
            var theta = random * Mathf.PI / 180f;
            var x = Mathf.Cos(theta) * _radius + clickObjectPosition.x;
            var y = Mathf.Sin(theta) * _radius + clickObjectPosition.y;
            return new Vector3(x, y, 0f);
        }
    }
}