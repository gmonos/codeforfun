using Microsoft.AspNetCore.Mvc;
using Zags.ProductFactory.Domain.Retrievers;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ZAGS.ProductFactory.WebAPI.Controllers
{
    [Route("api/products")]
    public class ProductSearchController : Controller
    {
        protected readonly IProductRetriever ProductRetriever;

        public ProductSearchController(IProductRetriever productRetriever)
        {
            ProductRetriever = productRetriever;
        }


        [HttpGet()]
        public IActionResult Search(string name)
        {
            var result = ProductRetriever.Search(name);

            return Ok(result);
        }
    }
}
