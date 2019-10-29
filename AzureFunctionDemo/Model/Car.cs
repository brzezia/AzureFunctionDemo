using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AzureFunctionDemo.Model
{
    public class Car : IEntity
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public int Milage { get; set; }
        public byte[] Documents { get; set; }
    }
}

