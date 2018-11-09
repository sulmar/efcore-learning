using System;

namespace EFCore.Models
{
    public class Service : Item
    {
        public TimeSpan Duration { get; set; }
    }
}
