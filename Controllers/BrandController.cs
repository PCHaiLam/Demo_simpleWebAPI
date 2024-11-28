using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Demo_simpleWebAPI.Models;

namespace Demo_simpleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        public readonly string connectionString;
        public BrandController(IConfiguration configuration)
        {
            connectionString = configuration["ConnectionStrings:ConnToDb"] ?? "";
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Brand> brands = new List<Brand>();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql= "SELECT * FROM brands";

                    using(var command = new SqlCommand(sql, connection))
                    {
                        using(var reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                Brand b = new Brand();

                                b.Id = reader.GetInt32(0);
                                b.Name = reader.GetString(1);

                                brands.Add(b);
                            }
                        }
                    }
                    return Ok(brands);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Brand brand = new Brand();
            try
            {
                using(var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM brands WHERE Id = @Id";

                    using(var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        using(var reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                brand.Id = reader.GetInt32(0);
                                brand.Name = reader.GetString(1);
                            }
                        }
                    }
                    return Ok(brand);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Create(BrandFG brandFG)
        {
            try
            {
                using(var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "INSERT INTO brands" + 
                        "(Name) VALUES " +
                        "(@Name)";

                    using(var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", brandFG.Name);

                        command.ExecuteNonQuery();
                    }
                    return Ok("Thêm thành công");
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BrandFG brandFG)
        {
            try
            {
                using(var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "UPDATE brands SET " +
                        "Name = @Name " +
                        "WHERE Id = @Id";

                    using(var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", brandFG.Name);
                        command.Parameters.AddWithValue("@Id", id);

                        command.ExecuteNonQuery();
                    }
                    return Ok("Cập nhật thành công");
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                using(var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "DELETE FROM brands WHERE Id = @Id";

                    using(var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        command.ExecuteNonQuery();
                    }
                    return Ok("Xóa thành công");
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
