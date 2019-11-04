using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunctionDemo.Model
{
    public class Pet : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
