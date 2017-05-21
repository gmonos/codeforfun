using NSpecifications;
using System;
using System.Collections.Generic;
using Zags.Utility.Errors;
using Zags.Utility.Functional;
using ZAGS.Domain.Validation;

namespace Zags.ProductFactory.Domain.Validations
{
    public class ProductValidator : IValidator<Product>
    {
        Specification<Product> _specName;
        Specification<Product> _specDate;


        public ProductValidator()
        {
            _specName = Spec.For<Product>(x => !String.IsNullOrEmpty(x.Name));
            _specDate = Spec.For<Product>(x => x.EffectiveDate > DateTime.MinValue);
        }

    

        public Either<IList<Error>, Product> Validate(Product product)
        {
            List<Error> errors = new List<Error>();

            if (!_specName.IsSatisfiedBy(product)) errors.Add("Product name is mandatory");
            if (!_specDate.IsSatisfiedBy(product)) errors.Add("Product effective date is mandatory");

            if (errors.Count > 0) return errors;

            return product;
        }
    }
}

