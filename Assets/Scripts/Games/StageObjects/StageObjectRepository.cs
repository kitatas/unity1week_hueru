using UnityEngine;
using Zenject;

namespace Games.StageObjects
{
    /// <summary>
    /// ふやすオブジェクトの生成クラス
    /// </summary>
    public sealed class StageObjectRepository : MonoBehaviour
    {
        private readonly float _radius = 0.125f;

        private GameObject[] _stageObjectList;

        [Inject]
        private void Construct(StageObjectTable stageObjectTable)
        {
            _stageObjectList = stageObjectTable.StageObjectList;
        }

        /// <summary>
        /// ふやすオブジェクトの生成
        /// </summary>
        /// <param name="clickObjectPosition"></param>
        public void GenerateStageObject(Vector3 clickObjectPosition)
        {
            var stageObject = Instantiate(GetStageObject(), transform);
            stageObject.transform.position = GetGeneratePosition(clickObjectPosition);
        }

        /// <summary>
        /// PUN用
        /// ふやすオブジェクトの名前取得
        /// </summary>
        /// <returns></returns>
        public string GetGenerateStageObjectName()
        {
            return GetStageObject().name;
        }

        /// <summary>
        /// ふやすオブジェクトの取得
        /// </summary>
        /// <returns></returns>
        private GameObject GetStageObject()
        {
            return _stageObjectList[Random.Range(0, _stageObjectList.Length)];
        }

        /// <summary>
        /// クリックした位置の近く
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