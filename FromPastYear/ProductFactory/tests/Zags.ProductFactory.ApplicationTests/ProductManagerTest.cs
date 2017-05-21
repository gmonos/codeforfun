using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using Zags.DataAccess.Dapper;
using Zags.OrganizationService.IntegrationTests;
using Zags.ProductFactory.Application.Managers;
using Zags.ProductFactory.Domain;
using Moq;
using Zags.DataAccess.Dapper;
using Zags.ProductFactory.Application.Managers;
using FluentAssertions;
using ZAGS.Domain.Validation;
using Microsoft.Extensions.Logging;
using Zags.ProductFactory.Application.Tests;
using ZAGS.Domain.Specification;

namespace Zags.ProductFactory.ApplicationTests
{
    [Collection("MemoryDataBase Collection")]
    public class ProductManagerTest
    {
        private readonly InMemoryDatabaseOrmLiteFixture _inMemoryDBFixture;
        public ProductManagerTest(InMemoryDatabaseOrmLiteFixture inMemoryDBFixture)
        {
            _inMemoryDBFixture = inMemoryDBFixture;
        }

        protected Mock<GenericDapperRepository<Product>> productRepositoryMock = new Mock<GenericDapperRepository<Product>>() { CallBase = true };
        protected Mock<IValidator<Product>> productValidatorMock = new Mock<IValidator<Product>>();
        protected Mock<ILogger<ProductManager>> loggerMock = new Mock<ILogger<ProductManager>>();
        protected Mock<IList<ISpecificationDispacher<Product>>> specificationsMock = new Mock<IList<ISpecificationDispacher<Product>>>();

        protected List<Product> Products { get; private set; }

        [Fact]
        [Trait("Category", "Integration Test")]
        public void GetProduct_WhenSearchProductWithId_ThenReturnProductWithData()
        {
            // Arrange
            SeedDataProduct();
            productRepositoryMock.Setup(repo => repo.CreateDbConnection(It.IsAny<string>()))
                .Returns(_inMemoryDBFixture.InMemoryDB.InMemoryDbConnection);

            var productManager = new ProductManager(productRepositoryMock.Object, productValidatorMock.Object, specificationsMock.Object, loggerMock.Object);

            // Act
            var product = productManager.GetProductById(1).Match<Product>(Left: (error) => { error.Should().BeNull(); return null; },
                Right: (prod) => { return prod; });

            // Assert
            product.Name.Should().Be("Santé Collective");
        }

        protected void SeedDataProduct()
        {
            Products = new List<Product>()
            {
                new Product(){ Name = "Santé Collective", EffectiveDate = new DateTime(2016,1,1), Status = ProductStatusEnum.Analyse, Id = 1 },
                new Product(){ Name = "Prévoyance", EffectiveDate = new DateTime(2017,1,1), Status = ProductStatusEnum.Test, Id = 2 }
            };

            _inMemoryDBFixture.InMemoryDB.Add(Products);
        }
    }
}
