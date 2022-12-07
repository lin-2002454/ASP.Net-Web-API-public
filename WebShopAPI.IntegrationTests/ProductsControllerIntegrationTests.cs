using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebShopAPI.Controllers;
using WebShopAPI.Models;
using Xunit;

namespace WebShopAPI.IntegrationTests
{
    public class ProductsControllerIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;

        public ProductsControllerIntegrationTests(TestingWebAppFactory<Program> factory)
            => _client = factory.CreateClient();

        [Fact]
        public async Task Get_All()
        {
            var response = await _client.GetAsync("api/Products");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Post_Test()
        {
            
            var response = await _client.PostAsync("api/Products", new StringContent( JsonConvert.SerializeObject(new Product{ ProductId = 1, ProductTitle = "Demo1", ProductDescription = "product1", ProductPrice = 20, ProductQuantity = 1, ProductImage = "Image.png", ProductSize = "S" }), Encoding.UTF8,"application/json" ));
            response.EnsureSuccessStatusCode();
            response.StatusCode.HasFlag(HttpStatusCode.OK);



        }

        [Fact]
        public async Task TestPostProduct_2()
        {
            // Arrange
            var request = new
            {
                Url = "/api/Products",
                Body = new
                
                { ProductId = 1, ProductTitle = "Demo1", ProductDescription = "product1", ProductPrice = 20, ProductQuantity = 1, ProductImage = "Image.png", ProductSize = "S" },
                
            };

            // Act
            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
           

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetProductById()
        {
            // Arrange
            var request = "/api/Products/";
            Product product = new Product { ProductId = 1, ProductTitle = "Demo1", ProductDescription = "product1", ProductPrice = 20, ProductQuantity = 1, ProductImage = "Image.png", ProductSize = "S", };

            // Act
            var response = await _client.GetAsync(request, (HttpCompletionOption)product.ProductId);

            // Assert
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task Delete_Test()
        {
            var request = "/api/Products";
            Product product = new Product { ProductId = 1, ProductTitle = "Demo1", ProductDescription = "product1", ProductPrice = 20, ProductQuantity = 1, ProductImage = "Image.png", ProductSize = "S", };

            var response = await _client.GetAsync(request, (HttpCompletionOption)product.ProductId);
            _ = await _client.DeleteAsync("api/Products/1");
            response.EnsureSuccessStatusCode();
  
        }

        [Fact]
        public async Task TestPutStockItemAsync()
        {
            var request = new
            {
                Url = "/api/Products",
                Body = new

                { ProductId = 1, ProductTitle = "Demo1", ProductDescription = "product1", ProductPrice = 20, ProductQuantity = 1, ProductImage = "Image.png", ProductSize = "S" },

            };
            Product product = new Product { ProductId = 1, ProductTitle = "Demo1", ProductDescription = "product1", ProductPrice = 20, ProductQuantity = 1, ProductImage = "Image.png", ProductSize = "S", };

            var response = await _client.GetAsync(request.Url, (HttpCompletionOption)product.ProductId);
            _ = await _client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            response.EnsureSuccessStatusCode();
        }

        //    [Fact]
        //    public async Task Create_WhenPOSTExecuted_ReturnsToIndexViewWithCreatedEmployee()
        //    {
        //        var postRequest = new HttpRequestMessage(HttpMethod.Post, "/s/Create");

        //        var formModel = new Dictionary<string,string >
        //{
        //    { "ProductTitle", "New Employee" },
        //    { "ProductDescription", "25" },
        //    { "ProductPrice", "25" },
        //    { "ProductQuantity", "1" },
        //    { "ProductImage", "Image.png" },
        //    { "ProductSize", "S" }
        //};

        //        postRequest.Content = new FormUrlEncodedContent(formModel);

        //        var response = await _client.SendAsync(postRequest);

        //        response.EnsureSuccessStatusCode();

        //        var responseString = await response.Content.ReadAsStringAsync();

        //        Assert.Contains("New Employee", responseString);
        //        Assert.Contains("25", responseString);
        //    }
    }



}