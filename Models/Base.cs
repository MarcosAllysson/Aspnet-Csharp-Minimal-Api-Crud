using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalApiCrud.Models
{
    public class Base
    {
        public Guid Id { get; init; }
        public DateTime CreatedAt { get; init; }

        public Base()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
    }
}