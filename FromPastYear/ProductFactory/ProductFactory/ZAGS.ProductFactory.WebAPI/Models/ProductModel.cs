using System;
using System.Collections.Generic;
using Zags.ProductFactory.Domain;

namespace ZAGS.ProductFactory.WebAPI.Models
{
    public class ProductModel
    {
        public ProductModel()
        {
        }

        public Int32 Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public DateTime EffectiveDate { get; set; }

        public List<CoverageModel> Coverages { get; set; }

        public List<PackModel> Packs { get; set; }
    }

}
