using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using AutoBogus;
using Bogus;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace NetCore.WebSockets.Demo.Server
{
    internal class Vehicles : WebSocketBehavior
    {
        private readonly Faker<Vehicle> _vehiclesFaker = new Faker<Vehicle>()
            .RuleFor(v => v.Vin, f => f.Vehicle.Vin())
            .RuleFor(v => v.Fuel, f => f.Vehicle.Fuel())
            .RuleFor(v => v.Make, f => f.Vehicle.Manufacturer())
            .RuleFor(v => v.Type, f => f.Vehicle.Type())
            .RuleFor(v => v.Model, f => f.Vehicle.Model());

        public Vehicles()
        {
            AutoFaker.Configure(builder =>
            {
                builder
                    .WithRecursiveDepth(4);
            });
        }

        private List<Vehicle> GenerateVehicles(int count) => AutoFaker.Generate<Vehicle>(count);

        protected override void OnOpen()
        {
            Parallel.ForEach(_vehiclesFaker.GenerateLazy(10), (v, _) => { Send(JsonSerializer.Serialize(v)); });
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            //{"Type":"Sedan"}
            VehicleFilter filter = null;
            try{
                filter = JsonSerializer.Deserialize<VehicleFilter>(e.Data);
            }
            catch (Exception)
            {
                // ignored
            }

            Parallel.ForEach(_vehiclesFaker.GenerateLazy(20_000), (v, _) =>
            {
                if (filter != null && !filter.Satisfies(v))
                    return;
                
                Send(JsonSerializer.Serialize(v));
            });
        }
    }
}