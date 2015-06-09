using System;
using System.Collections.Generic;

namespace StructureComparer.Tests.Fakes.StructureA
{
    public class FakeCustomerWithDifferentPropertyName
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public FakeEnum Enum { get; set; }

        public IEnumerable<int> Identifiers { get; set; }
    }
}
