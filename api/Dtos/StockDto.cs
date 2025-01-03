using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static api.Constants.Messages;

namespace api.Dtos;
public class StockDto
{
    [JsonIgnore]
    public int Id { get; set; }
    [Required]
    [MinLength(1, ErrorMessage = TextTooShort)]
    [MaxLength(10, ErrorMessage = TextTooLong)]
    public string Symbol { get; set; } = string.Empty;
    [Required]
    [MinLength(1, ErrorMessage = TextTooShort)]
    [MaxLength(50, ErrorMessage = TextTooLong)]
    public string CompanyName { get; set; } = string.Empty;
    [Required]
    [Range(1, 1000000000)]
    public decimal Purchase { get; set; }
    [Required]
    [Range(0.001, 100)]
    public decimal LastDiv { get; set; }
    [Required]
    [MinLength(1, ErrorMessage = TextTooShort)]
    [MaxLength(50, ErrorMessage = TextTooLong)]
    public string Industry { get; set; } = string.Empty;
    [Required]
    [Range(1, 5000000000)]
    public long MarketCap { get; set; }
    [JsonIgnore]
    public IEnumerable<CommentDto> Comments { get; set; } = [];
}