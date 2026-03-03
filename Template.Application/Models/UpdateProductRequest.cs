using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Template.Application.Models
{
    public class UpdateProductRequest
    {
        [Required(ErrorMessage = "Product name is required.")]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required]
        [Range(1, 1000000000, ErrorMessage = "Not a Valid Price")]
        public decimal Price { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
