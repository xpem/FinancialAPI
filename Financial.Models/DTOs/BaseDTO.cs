using System.ComponentModel.DataAnnotations;

namespace Financial.Models.DTOs
{
    public class BaseDTO
    {
        public int Id { get; set; }

        public required DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public int? UserId { get; set; }
    }
}
