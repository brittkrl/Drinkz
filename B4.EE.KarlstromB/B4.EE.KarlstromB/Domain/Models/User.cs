using System;
using System.Collections.Generic;
using System.Text;

namespace B4.EE.KarlstromB.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
