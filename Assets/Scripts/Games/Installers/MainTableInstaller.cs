using Games.StageObjects;
using UnityEngine;
using Zenject;

namespace Games.Installers
{
    [CreateAssetMenu(fileName = "MainTableInstaller", menuName = "Installers/MainTableInstaller")]
    public sealed class MainTableInstaller : ScriptableObjectInstaller<MainTableInstaller>
    {
        [SerializeField] private StageObjectTable stageObjectTable = null;

        public override void InstallBindings()
        {
            Container
                .BindInstance(stageObjectTable)
                .AsCached();
        }
    }
}