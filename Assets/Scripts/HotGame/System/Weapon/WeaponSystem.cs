using ECS;
using PureMVC.Interfaces;
using PureMVC.Patterns.Facade;

public class WeaponSystem : SystemBase<Weapon>
{
    protected IFacade Facade { get { return PureMVC.Patterns.Facade.Facade.GetInstance(() => new Facade()); } }

    protected override void OnUpdate(int index, Entity entity, Weapon weapon)
    {
        //if (weapon.fire)
        //{
        //    if (weapon.state >= SpawnState.None && weapon.state < SpawnState.Finish)
        //    {
        //        weapon.timer += Time.DeltaTime;
        //        if (weapon.timer > weapon.duration)
        //        {
        //            weapon.state = SpawnState.Start;
        //            weapon.timer = 0;
        //        }
        //    }
        //}
    }

}
