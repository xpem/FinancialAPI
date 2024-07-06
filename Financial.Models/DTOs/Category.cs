using System.ComponentModel.DataAnnotations;
using static Financial.Models.TransactionTypes;

namespace Financial.Models.DTOs
{
    public class Category : BaseDTO
    {
        public bool SystemDefault { get; set; } = false;

        [MaxLength(50)]
        public required string Name { get; set; }

        [MaxLength(8)]
        public required string Color { get; set; }

        public required TransactionType Type { get; set; }

        //[MaxLength(100)]
        //public string? IconName { get; set; }
    }
}
