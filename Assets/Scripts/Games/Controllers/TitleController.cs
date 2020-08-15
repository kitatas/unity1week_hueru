using Configs;
using Games.Sounds;
using UnityEngine;
using Zenject;

namespace Games.Controllers
{
    public sealed class TitleController : MonoBehaviour
    {
        [Inject]
        private void Construct(BgmController bgmController)
        {
            bgmController.PlayBgm(BgmType.Main);
        }
    }
}