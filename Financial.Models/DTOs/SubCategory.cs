using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Financial.Models.DTOs
{
    public class SubCategory : BaseDTO
    {
        public required int CategoryId { get; set; }

        [MaxLength(50)]
        public required string Name { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }

        //[MaxLength(100)]
        //public string? IconName { get; set; }
    }
}
