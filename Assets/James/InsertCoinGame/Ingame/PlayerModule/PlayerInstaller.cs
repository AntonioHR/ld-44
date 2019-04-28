using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace James.InsertCoinGame.Ingame.PlayerModule
{
    public class PlayerInstaller : MonoInstaller
    {
        public PlayerBody body;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Player>().AsSingle();
            Container.Bind<PlayerBody>().FromInstance(body);
            Container.Bind<Player.Fsm>().AsSingle();
        }
    }
}
