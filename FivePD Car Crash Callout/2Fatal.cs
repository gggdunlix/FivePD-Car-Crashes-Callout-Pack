﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;
using CitizenFX.Core.Native;



[CalloutProperties("Vehicle Collision (2 Deaths)", "GGGDunlix", "3.0.0")]
public class CarCrash2Fatal : FivePD.API.Callout
{
    private Ped driver1, driver2;
    private Vehicle car1, car2;
    public CarCrash2Fatal()
    {

        InitInfo(World.GetNextPositionOnStreet(Vector3Extension.Around(Game.PlayerPed.Position, 400)));

        ShortName = "Vehicle Collision";
        CalloutDescription = "A vehicle collision has occured with unknown injuries. Get the location and assess. Respond in Code 3.";
        ResponseCode = 3;
        StartDistance = 150f;
    }

    public override async Task OnAccept()
    {
        InitBlip(25);
        UpdateData();
        
    }

    public async override void OnStart(Ped player)
    {
        base.OnStart(player);
        var cars = new[]
          {
               VehicleHash.Adder,
               VehicleHash.CarbonRS,
               VehicleHash.Oracle,
               VehicleHash.Oracle2,
               VehicleHash.Phoenix,
               VehicleHash.Vigero,
               VehicleHash.Zentorno,
               VehicleHash.Youga2,
               VehicleHash.Youga,
               VehicleHash.Sultan,
               VehicleHash.SultanRS,
               VehicleHash.Sentinel,
               VehicleHash.Sentinel2,
               VehicleHash.Ruiner,
               VehicleHash.Ruiner2,
               VehicleHash.Ruiner3,
               VehicleHash.Burrito,
               VehicleHash.Burrito2,
               VehicleHash.Burrito3,
               VehicleHash.GBurrito,
               VehicleHash.Bagger,
               VehicleHash.Buffalo,
               VehicleHash.Buffalo2,
               VehicleHash.Comet2,
               VehicleHash.Comet3,
               VehicleHash.Felon,
           };

        car1 = await SpawnVehicle(cars[RandomUtils.Random.Next(cars.Length)], Location, 180);
        car2 = await SpawnVehicle(cars[RandomUtils.Random.Next(cars.Length)], Location);
        World.ShootBullet(Location, car1.Position, Game.PlayerPed, WeaponHash.RayPistol, 0);
        World.ShootBullet(Location, car1.Position, Game.PlayerPed, WeaponHash.RayPistol, 0);
        car1.Deform(Location, 10000, 100);
        car2.Deform(Location, 10000, 100);
        car1.EngineHealth = 5;
        car2.EngineHealth = 5;
        car1.BodyHealth = 1;
        car2.BodyHealth = 2;

        API.Wait(2);

        driver1 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 5);
        driver2 = await SpawnPed(RandomUtils.GetRandomPed(), Location + 6, 180);

        driver1.AlwaysKeepTask = true;
        driver1.BlockPermanentEvents = true;

        driver2.AlwaysKeepTask = true;
        driver2.BlockPermanentEvents = true;

        driver1.SetIntoVehicle(car1, VehicleSeat.Driver);
        driver1.Kill();
        driver2.SetIntoVehicle(car2, VehicleSeat.Driver);
        driver2.Kill();
        car1.Deform(Location, 10000, 100);
        car2.Deform(Location, 10000, 100);
        World.ShootBullet(Location, car1.Position, Game.PlayerPed, WeaponHash.RayPistol, 0);
        driver1.AttachBlip();
        driver2.AttachBlip();
        car1.AttachBlip();
        car2.AttachBlip();
        
    }
}