using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Zags.ProductFactory.Application.Managers;
using Zags.ProductFactory.Domain;
using ZAGS.ProductFactory.WebAPI.Controllers;
using ZAGS.ProductFactory.WebAPI.Models;

namespace Zags.ProductFactory.Application.Tests
{
    public class ProductManagementControllerFixture : IDisposable
    {
        public ProductManagementController Controller { get; private set; }
        public Mock<IProductManager> ProductManagerMoq { get; private set; }
        public ProductManagementControllerFixture()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductModel>();
            });

            ProductManagerMoq = new Mock<IProductManager>();
            Controller = new ProductManagementController(ProductManagerMoq.Object);
        }

        public void Dispose()
        {
            Controller.Dispose();
        }
    }
}
