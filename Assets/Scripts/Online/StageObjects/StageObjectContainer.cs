using System.Collections.Generic;
using UnityEngine;

namespace Online.StageObjects
{
    /// <summary>
    ///  ふやしたオブジェクトの管理クラス
    /// </summary>
    public sealed class StageObjectContainer
    {
        private readonly List<Rigidbody2D> _stageObjectList;

        public StageObjectContainer()
        {
            _stageObjectList = new List<Rigidbody2D>();
        }

        /// <summary>
        /// リストに追加
        /// </summary>
        /// <param name="stageObject"></param>
        public void AddStageObject(OnlineStageObject stageObject)
        {
            var rb = stageObject.GetComponent<Rigidbody2D>();
            _stageObjectList.Add(rb);
        }

        /// <summary>
        /// 全て静止しているかの判定
        /// </summary>
        /// <returns></returns>
        public bool IsAllSleep()
        {
            foreach (var stageObject in _stageObjectList)
            {
                if (stageObject == null)
                {
                    continue;
                }

                if (stageObject.velocity.magnitude > 0.001f)
                {
                    return false;
                }
            }

            return true;
        }
    }
}