using System.ComponentModel.DataAnnotations;

namespace Demo_simpleWebAPI.Models
{
    public class BrandFG
    {
        [Required]
        public string Name { get; set; } = "";
    }
    public class Brand : BrandFG
    {
        public int Id { get; set; }
    }
}
