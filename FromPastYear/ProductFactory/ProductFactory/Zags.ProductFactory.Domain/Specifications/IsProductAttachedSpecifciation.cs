using NSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Zags.ProductFactory.Domain.Retrievers;
using ZAGS.Domain.Specification;

namespace Zags.ProductFactory.Domain.Validations
{
    public class IsProductAttachedSpecifciation :Specification<Product>, ISpecificationDispacher<Product>
    {
        public IsProductAttachedSpecifciation()
        {
           
        }

        public override Expression<Func<Product, bool>> Expression
        {
            get => (product => !product.Packs.Any());
        }


        public IList<EnumAction> Actions { get; } = new List<EnumAction> { EnumAction.Deletion };

        public string ErrorMessage { get; } = "The product cannot be deleted because it still contains packs";
    }
}

