namespace NetCore.WebSockets.Demo.Server
{
    class VehicleFilter
    {
        public string Type { get; set; }
        public string Model { get; set; }

        public bool Satisfies(Vehicle vehicle)
        {
            // ReSharper disable once ReplaceWithSingleAssignment.True
            var satisfies = true;
            
            if (!string.IsNullOrEmpty(Model) && Model != vehicle.Model)
                satisfies = false;
            
            if (!string.IsNullOrEmpty(Type) && Type != vehicle.Type)
                satisfies = false;

            return satisfies;
        }
    }
}