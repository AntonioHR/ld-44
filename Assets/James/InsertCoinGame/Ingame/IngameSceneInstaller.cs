using System;
using James.InsertCoinGame.Ingame;
using James.InsertCoinGame.Ingame.PlayerModule;
using James.InsertCoinGame.Ingame.Ui;
using UnityEngine;
using Zenject;

public class IngameSceneInstaller : MonoInstaller
{
    public InsertCoinIngameScene scene;
    public InsertCoinUI ui;
    public PlayerBody playerPrefab;
    public override void InstallBindings()
    {
        Container.Bind<InsertCoinIngameScene>().FromInstance(scene);
        Container.Bind<InsertCoinUI>().FromInstance(ui);
        Container.Bind<Player>().FromSubContainerResolve().ByNewContextPrefab(playerPrefab).UnderTransformGroup("Units").AsSingle();
        BindIngameFsm();
    }

    private void BindIngameFsm()
    {
        InsertCoinIngameScene.BindAllStateFactories(Container);
    }
}