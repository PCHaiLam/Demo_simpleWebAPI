using Demo_simpleWebAPI.Models;
using Demo_simpleWebAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace Demo_simpleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public static List<Models.Product> products = new List<Product>();
        private readonly string connectionString;

        public ProductsController(IConfiguration configuration)
        {
            connectionString = configuration["ConnectionStrings:ConnToDb"] ?? "";
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(products);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var product = products.FirstOrDefault(x => x.Id == Guid.Parse(id));
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IActionResult Create(ProductFG productFG)
        {
            var newProduct = new Models.Product
            {
                Id = Guid.NewGuid(),
                Name = productFG.Name,
                Price = productFG.Price
            };

            products.Add(newProduct);
            return Ok(new
            {
                Success = true,
                data = newProduct
            });
        }
        [HttpPut("{id}")]
        public IActionResult Update(string id, Models.Product productEdit)
        {
            try
            {
                var product = products.FirstOrDefault(x => x.Id == Guid.Parse(id));
                if (product == null)
                {
                    return NotFound();
                }

                if(id != productEdit.Id.ToString())
                {
                    return BadRequest();
                }

                product.Name = productEdit.Name;
                product.Price = productEdit.Price;

                return Ok(new
                {
                    Success = true,
                    data = product
                });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var product = products.FirstOrDefault(x => x.Id == Guid.Parse(id));
                if (product == null)
                {
                    return NotFound();
                }

                products.Remove(product);

                return Ok(new
                {
                    Success = true,
                    data = product
                });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
