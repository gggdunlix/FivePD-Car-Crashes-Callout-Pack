using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;
using CitizenFX.Core.Native;



[CalloutProperties("Vehicle Collision", "GGGDunlix", "0.0.1")]
public class CarCrash : FivePD.API.Callout
{
    private Ped driver1, driver2;
    private Vehicle car1, car2;
    public CarCrash()
    {

        InitInfo(World.GetNextPositionOnStreet(Vector3Extension.Around(Game.PlayerPed.Position, 200)));

        ShortName = "Vehicle Collision";
        CalloutDescription = "A vehicle collision has occured. Get the location and assess. ";
        ResponseCode = 2;
        StartDistance = 100f;
    }

    public override async Task OnAccept()
    {
        InitBlip(25);

        driver1 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 1);
        driver2 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 1);

        car1 = await SpawnVehicle(RandomUtils.GetRandomVehicle(), Location);
        car2 = await SpawnVehicle(RandomUtils.GetRandomVehicle(), Location);

        car1.Deform(Location, 100, 10);
        car2.Deform(Location, 100, 10);

    }

    public override void OnStart(Ped player)
    {
        base.OnStart(player);
    }
}