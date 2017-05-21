using Moq;
using Xunit;
using ZAGS.ProductFactory.WebAPI.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Zags.ProductFactory.Domain;
using Zags.ProductFactory.Application.Tests;

namespace Zags.ProductFactory.ApplicationTests
{
    [Trait("Category", "API")]
    public class ProductManagementControllerTest : IClassFixture<ProductManagementControllerFixture>
    {
        private readonly ProductManagementControllerFixture _fixture;
        public ProductManagementControllerTest(ProductManagementControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void CreateProduct_WhenProductModelIsNull_ThenHttpResponseIsBadRequest()
        {
            // Arrange      
            ProductModel model = null;

            // Act
            var result = _fixture.Controller.CreateProduct(model);

            // Assert   
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void Index_WhenIdProductIsNotNull_ThenProductIsFound()
        {
            // Arrange
            
            var product = new Product() { Id = 1, Name = "Santé" };
            _fixture.ProductManagerMoq.Setup(mgr => mgr.GetProductById(It.IsAny<int>())).Returns(product);

            // Act
            var result = _fixture.Controller.Index(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
