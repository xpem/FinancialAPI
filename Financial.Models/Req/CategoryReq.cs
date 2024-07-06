using System.ComponentModel.DataAnnotations;

namespace Financial.Models.Req
{
    public record CategoryReq : BaseRequest
    {
        [StringLength(50)]
        [Required]
        public required string Name { get; init; }

        [StringLength(8)]
        [Required]
        public string Color { get; init; } = "3C3C3C";

        [Required]
        [RegularExpression("[0|1]", ErrorMessage = "Enter 0 or 1 only")]
        public TransactionTypes.TransactionType Type { get; init; }
    }
}
