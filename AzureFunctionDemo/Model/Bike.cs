using System;

namespace AzureFunctionDemo.Model
{
    public class Bike : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Date { get; set; }
    }
}