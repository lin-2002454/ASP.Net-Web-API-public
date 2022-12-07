using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebShopAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Infrastructure;


namespace WebShopAPI.Controllers.Tests
{
    [TestClass()]
    public class ProductsControllerTests
    {

        [TestMethod()]
        public void GetAllProductsTest_Should_ReturnAllProducts()
        {
            //Arrange
            var productContextMock = new Mock<ProductContext>();
      
            var data = new List<Product>
            {
                new Product { ProductId = 1, ProductTitle = "Demo1", ProductDescription = "product1",ProductPrice = 20, ProductQuantity = 1, ProductImage = "Image.png", ProductSize = "S"  },
                new Product { ProductId = 2, ProductTitle = "Demo2", ProductDescription = "product2", ProductPrice = 20, ProductQuantity = 1, ProductImage = "Image.png", ProductSize = "M" },
                new Product { ProductId = 3, ProductTitle = "Demo2", ProductDescription = "product3", ProductPrice = 20, ProductQuantity = 1, ProductImage = "Image.png", ProductSize = "L" },
            }.AsQueryable();
            //Act
            var controller = new ProductsController(productContextMock.Object);
            var products = controller.GetProducts();
            //Assert
            Assert.IsNotNull(products);
       
        }
        [TestMethod]
        public void CreateProduct_via_context()
        {
            //Arrange
            var productContextMock = new Mock<ProductContext>();
            //Act
            var controller = new ProductsController(productContextMock.Object);
            var products = controller.PostProduct(new Product { ProductId = 1, ProductTitle = "Demo1", ProductDescription = "product1", ProductPrice = 20, ProductQuantity = 1, ProductImage = "Image.png", ProductSize = "S" });
            //Assert
            Assert.IsNotNull(products);
        }

        [TestMethod]
        public void DeleteProduct_ProductShouldbeReturned()
        {
            //Arrange
            Product product = new Product { ProductId = 1, ProductTitle = "Demo1", ProductDescription = "product1", ProductPrice = 20, ProductQuantity = 1, ProductImage = "Image.png", ProductSize = "S", };
            var productContextMock = new Mock<ProductContext>();
            //Act
            var controller = new ProductsController(productContextMock.Object);
            _ = controller.DeleteProduct(product.ProductId);
            //Assert
            Assert.IsNotNull(productContextMock.Object);
            Assert.IsNotNull(product.ProductId);

           // var products = controller.PostProduct(new Product { ProductId = 1, ProductTitle = "Demo1", ProductDescription = "product1", ProductPrice = 20, ProductQuantity = 1, ProductImage = "Image.png", ProductSize = "S" });
           
        }

        [TestMethod]

        public void UpdateProduct_ProductShouldbeReturned()
        {
            //Arrange
            Product product = new Product { ProductId = 1, ProductTitle = "Demo1", ProductDescription = "product1", ProductPrice = 20, ProductQuantity = 1, ProductImage = "Image.png", ProductSize = "S", };
            var productContextMock = new Mock<ProductContext>();
            //Act
            var controller = new ProductsController(productContextMock.Object);
           _= controller.PutProduct(product.ProductId, new Product { ProductId = 2, ProductTitle = "Demo2", ProductDescription = "product2", ProductPrice = 30, ProductQuantity = 2, ProductImage = "Image.png", ProductSize = "M" });
           //Assert
            Assert.IsNotNull(productContextMock.Object);
        }

        [TestMethod]

        public void GetProductById_ShouldReturnOneProduct()
        {
            //Arrange
            Product product = new Product { ProductId = 1, ProductTitle = "Demo1", ProductDescription = "product1", ProductPrice = 20, ProductQuantity = 1, ProductImage = "Image.png", ProductSize = "S", };
            var productContextMock = new Mock<ProductContext>();
            //Act
            var controller = new ProductsController(productContextMock.Object);
            _ = controller.GetProduct(product.ProductId);
            //Assert
            Assert.IsNotNull(product);
        }
        [TestMethod]
        public void GetAllProductsTest_ShouldReturnProducts()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ProductContext>()
                .UseInMemoryDatabase(databaseName: "ProductListDatabase")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new ProductContext(options))
            {
                context.Products.Add(new Product { ProductId = 1, ProductTitle = "Demo1", ProductDescription = "product1", ProductPrice = 20, ProductQuantity = 1, ProductImage = "Image.png", ProductSize = "S" });
                context.Products.Add(new Product { ProductId = 2, ProductTitle = "Demo2", ProductDescription = "product2", ProductPrice = 20, ProductQuantity = 1, ProductImage = "Image.png", ProductSize = "M" });
                context.Products.Add(new Product { ProductId = 3, ProductTitle = "Demo2", ProductDescription = "product3", ProductPrice = 20, ProductQuantity = 1, ProductImage = "Image.png", ProductSize = "L" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new ProductContext(options))
            {
                ProductsController controller = new ProductsController(context);
                //Act
                var products = controller.GetProducts();
               //Assert
               Assert.IsNotNull(context.Products);
               Assert.IsNotNull(products);
            }
        }
    }
}