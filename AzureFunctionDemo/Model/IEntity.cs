using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunctionDemo.Model
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
