using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Zags.ProductFactory.Application.Managers;
using Zags.ProductFactory.Domain;
using ZAGS.ProductFactory.WebAPI.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ZAGS.ProductFactory.WebAPI.Controllers
{
    [Route("v1/products")]
    public class ProductManagementController : Controller
    {
        private readonly IProductManager _productManager;

        public ProductManagementController(IProductManager productManager)
        {
            _productManager = productManager;
        }

   
        [HttpGet("{id:int}")]
        public IActionResult Index(int id)
        {
            return _productManager.GetProductById(id).Match<IActionResult>(Right: result => Ok(Mapper.Map<ProductModel>(result)),
                                                                           Left: error => NotFound());
        }



        [HttpPost()]
        public IActionResult CreateProduct([FromBody] ProductModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = Mapper.Map<Product>(model);

            return _productManager.CreateProduct(product)
                                  .Match<IActionResult>(
                                           Right: result => Ok(Mapper.Map<ProductModel>(result)),
                                           Left: error => StatusCode((int)HttpStatusCode.MethodNotAllowed, error) );
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]ProductModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = Mapper.Map<Product>(model);

            return _productManager.UpdateProduct(product).Match<IActionResult>(
                                           Right: result => NoContent(),
                                           Left: error => StatusCode((int)HttpStatusCode.MethodNotAllowed, error));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _productManager.DeleteProduct(id)
                                   .Match<IActionResult>(
                                           Right: result => NoContent(),
                                           Left: error => StatusCode((int)HttpStatusCode.MethodNotAllowed, error));

        }
    }
}
