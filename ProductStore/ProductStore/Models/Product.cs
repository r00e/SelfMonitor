using System.ComponentModel.DataAnnotations;

namespace ProductStore.Models
{

    public class Product
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal PageCount { get; set; }
    }
}