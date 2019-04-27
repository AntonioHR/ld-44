using James.InsertCoinGame.Ingame.InputModule;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameConfigsInstaller", menuName = "InsertCoin/GameConfigsInstaller")]
public class GameConfigsInstaller : ScriptableObjectInstaller<GameConfigsInstaller>
{
    public InputConfigs configsAsset;
    public override void InstallBindings()
    {
        Container.Bind<InputConfigs>().FromInstance(configsAsset).WhenInjectedInto<InsertCoinInput>();
        Container.Bind<InsertCoinInput>().AsSingle();
    }
}