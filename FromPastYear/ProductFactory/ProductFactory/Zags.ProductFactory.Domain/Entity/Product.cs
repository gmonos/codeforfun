using System;
using System.Collections.Generic;
using Zags.Domain.Entity;

namespace Zags.ProductFactory.Domain
{
    public class Product: EntityBase
    {
       

        public string Name { get; set; }

        public ProductStatusEnum Status { get; set; }

        public DateTime EffectiveDate { get; set; }

        public List<Coverage> Coverages { get; set; }

        public List<Pack> Packs { get; set; }
    }
}
