using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs
{
    public class CountryDto
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; } = null!;
    }
}