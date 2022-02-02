using Dapper;
using DapperExampleAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperExampleAPI.Controllers
{
    [ApiController]
    [Route("/api")]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _Configuration;
        private readonly string _ConnectionString;

        public ProductController(IConfiguration configuration)
        {
            _Configuration = configuration;
            _ConnectionString = _Configuration.GetSection("ConnectionStrings:LM_Stocks").Value;
        }

        [HttpPost]
        [Route("/products")]
        public IActionResult AddProduct(Product product)
        {
            using (var connection = new SqlConnection(_ConnectionString))
            {
                var sql = new StringBuilder();
                sql.AppendLine("INSERT INTO Products(Name, Price, Validity, Lot, Weight, Quantity, Description) VALUES (@Name, @Price, @Validity, @Lot, @Weight, @Quantity, @Description)");

                var rowsAffected = connection.Execute(sql.ToString(), product);

                return StatusCode(200, rowsAffected);
            }
        }

        [HttpPut]
        [Route("/products/{idProduct}")]
        public IActionResult UpdateProduct(int idProduct, Product product)
        {
            using (var connection = new SqlConnection(_ConnectionString))
            {
                var sql = new StringBuilder();
                sql.AppendLine("UPDATE Products SET Name = @Name, Validity = @Validity, Lot = @Lot, Weight = @Weight, Quantity = @Quantity, Description = @Description");
                sql.AppendLine($"WHERE Id = '{idProduct}'");

                var rowsAffected = connection.Execute(sql.ToString(), product);

                return StatusCode(200, rowsAffected);
            }
        }

        [HttpDelete]
        [Route("/products/{idProduct}")]
        public IActionResult DeleteProduct(int idProduct)
        {
            using (var connection = new SqlConnection(_ConnectionString))
            {
                var sql = new StringBuilder();
                sql.AppendLine("DELETE FROM Products");
                sql.AppendLine($"WHERE Id = '{idProduct}'");

                var rowsAffected = connection.Execute(sql.ToString());

                return StatusCode(200, rowsAffected);
            }
        }

        [HttpGet]
        [Route("/products/{idProduct}")]
        public IActionResult GetProductById(int idProduct)
        {
            using (var connection = new SqlConnection(_ConnectionString))
            {
                var product = connection.Query<Product>($"SELECT * FROM Products WHERE Id = '{idProduct}'");

                return StatusCode(200, product);
            }
        }

        [HttpGet]
        [Route("/products")]
        public IActionResult GetProducts()
        {
            using (var connection = new SqlConnection(_ConnectionString))
            {
                var products = connection.Query<Product>("SELECT * FROM Products");

                return StatusCode(200, products);
            }
        }
    }
}
