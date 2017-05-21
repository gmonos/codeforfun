using System.Collections.Generic;
using Zags.Domain.Entity;

namespace Zags.ProductFactory.Domain
{
    public class Pack: EntityBase
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public List<Coverage> Coverages { get; set; }

    }
}
