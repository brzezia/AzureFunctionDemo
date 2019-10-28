using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunctionDemo.Model
{
    public class Car
    {
        public Guid Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public int Milage { get; set; }
        public byte[] Documents { get; set; }
    }
}

