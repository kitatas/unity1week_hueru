using Games.Sounds;
using Games.StageObjects;
using UnityEngine;
using Zenject;

namespace Games.Installers
{
    [CreateAssetMenu(fileName = "MainTableInstaller", menuName = "Installers/MainTableInstaller")]
    public sealed class MainTableInstaller : ScriptableObjectInstaller<MainTableInstaller>
    {
        [SerializeField] private StageObjectTable stageObjectTable = null;
        [SerializeField] private BgmTable bgmTable = null;
        [SerializeField] private SeTale seTale = null;

        public override void InstallBindings()
        {
            Container
                .BindInstance(stageObjectTable)
                .AsCached();

            Container
                .BindInstance(bgmTable)
                .AsCached();

            Container
                .BindInstance(seTale)
                .AsCached();
        }
    }
}