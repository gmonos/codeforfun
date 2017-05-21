using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Zags.Domain.Repositories;
using Zags.Logging;
using Zags.Logging.Events;
using Zags.ProductFactory.Domain;
using Zags.Utility.Errors;
using Zags.Utility.Functional;
using ZAGS.Domain.Specification;
using ZAGS.Domain.Validation;

namespace Zags.ProductFactory.Application.Managers
{
    public class ProductManager: IProductManager
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IValidator<Product> _productValidator;
        private readonly IList<ISpecificationDispacher<Product>> _specifications;
        private readonly ILogger _logger;

        public ProductManager(IRepository<Product> productRepository, 
                              IValidator<Product> validator, 
                              IEnumerable<ISpecificationDispacher<Product>> specifications,
                              ILogger<ProductManager> logger)
        {
            _productRepository = productRepository;

            _productValidator = validator;

            _specifications = specifications.ToList() ;

            _logger = logger;
        }

        virtual public Either<Error, Product>  GetProductById(int id)
        {
            Option<Product> productOption = _productRepository.GetById(id);

            return productOption.Match<Either<Error, Product>>(
                None:() =>{
                            _logger.LogEvent(new ResourceNotFoundEvent<Product>(id));
                            return F.Error($"Product with Id {id} is not found");}, 
                Some:(p => p));
 
        }

        virtual  public Either<IList<Error>, Product> CreateProduct(Product product)
        {
            return _productValidator.Validate(product)
                                    .ApplySpecifications(_specifications.Where(x => x.Actions.Contains(EnumAction.Creation)))
                                    .Match<Either<IList<Error>, Product>>(
                                        Right: (p) =>
                                        {
                                            _productRepository.Add(product);

                                            _logger.LogEvent(new DomainTrakingEvent<Product>(product, EnumDomainActionType.Creation));
                                            return p;
                                        },
                                        Left: (errors) =>
                                        {
                                            _logger.LogEvent(new ValidationErrorEvent<Product>( product, EnumDomainActionType.Creation,errors));
                                            return errors.ToList();
                                        });
        }

        

        virtual public Either<IList<Error>, Unit> UpdateProduct(Product product)
        {
            var specifications = _specifications.Where(x => x.Actions.Contains(EnumAction.Modification));

            return _productValidator.Validate(product)
                                    .ApplySpecifications(specifications)
                                    .Match<Either<IList<Error>, Unit>>(
                                        Right: (p) =>
                                        {
                                            _productRepository.Update(p);

                                            _logger.LogEvent(new DomainTrakingEvent<Product>(product, EnumDomainActionType.Modification));

                                            return F.Unit();
                                        },
                                        Left: (errors) =>
                                        {
                                            _logger.LogEvent(new ValidationErrorEvent<Product>( product, EnumDomainActionType.Modification,errors));
                                            return errors.ToList();
                                        });

        }


        virtual public Either<IList<Error>, Unit> DeleteProduct(int id)
        {
            List<Error> errors;

            //Read the product to delete
            return _productRepository.GetById(id).Match<Either<IList<Error>, Unit>>(
                None: ()=> {
                    errors = new List<Error> { F.Error($"Product with id {id} does not exists") };

                    _logger.LogEvent(new ValidationErrorEvent<Product>( id, EnumDomainActionType.Deletion, errors));

                    return errors;
                },
                Some: (product) => {

                    //Check Specifications
                    errors = _specifications.Where(x => x.Actions.Contains(EnumAction.Deletion) && !x.IsSatisfiedBy(product))
                   .Select(x => F.Error(x.ErrorMessage))
                   .ToList();

                    //Porceed to the product deletion if no errors, otherwise return detected errors
                    if (errors.Count == 0)
                    {
                        _productRepository.Delete(product);

                        _logger.LogEvent(new DomainTrakingEvent<Product>(product, EnumDomainActionType.Deletion));

                        return F.Unit();
                    }
                    else
                    {
                        _logger.LogEvent(new ValidationErrorEvent<Product>(product, EnumDomainActionType.Deletion, errors));

                        return errors;
                    }
                });
            
           

            
        }


              
    }
}
