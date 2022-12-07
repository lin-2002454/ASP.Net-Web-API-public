using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebShopAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopAPI.Models;
using WebShopAPI.Controllers;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace WebShopAPI.Controllers.Tests
{
    [TestClass()]
    public class ProductsControllerTests
    {
        private readonly ProductContext _dbContext;
        public ProductsControllerTests(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        [TestMethod()]
        public void GetProductsTest()
        {
            //Arrange
            int count = 5;
            var dataStore = A.Fake<DbContext>();
            A.CallTo(() => dataStore.GetProducts(count)).Returns(typeof(ProductContext));
            ProductsController controller = new ProductsController(dataStore);
        }
    }
}