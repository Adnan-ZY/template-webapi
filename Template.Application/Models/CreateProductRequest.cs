using System.ComponentModel.DataAnnotations;

namespace Template.Application.Models
{
    public class CreateProductRequest
    {
        [Required(ErrorMessage = "Product name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string? Name { get; set; }

        [Required]
        [Range(1,1000000000, ErrorMessage = "Not a Valid Price")]
        public decimal Price { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }
    }
}
