using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ItemService>().AsSingle();
        Container.BindInterfacesAndSelfTo<DoorLockService>().AsSingle();
    }
}