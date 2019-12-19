using System;
using System.Collections.Generic;
using System.Text;

namespace B4.EE.KarlstromB.Domain.Models
{
    public class Ingredient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Amount { get; set; }
        public string OptionalAmount { get; set; }
    }
}
