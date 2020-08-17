using System.Collections.Generic;
using UnityEngine;

namespace Online.StageObjects
{
    public sealed class StageObjectContainer
    {
        private readonly List<Rigidbody2D> _stageObjectList;

        public StageObjectContainer()
        {
            _stageObjectList = new List<Rigidbody2D>();
        }

        public void AddStageObject(GameObject stageObject)
        {
            var rb = stageObject.GetComponent<Rigidbody2D>();
            _stageObjectList.Add(rb);
        }

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