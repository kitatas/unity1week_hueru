using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Online.Controllers
{
    public class GameController : MonoBehaviour
    {
        public IObservable<Unit> PlayingAsObservable =>
            this.UpdateAsObservable();
    }
}