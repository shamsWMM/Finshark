using System.ComponentModel.DataAnnotations;

namespace api.Dtos;
public class StockDto
{
    [Required]
    public string Symbol { get; set; } = string.Empty;
    [Required]
    public string CompanyName { get; set; } = string.Empty;
    public decimal Purchase { get; set; }
    public decimal LastDiv { get; set; }
    [Required]
    public string Industry { get; set; } = string.Empty;
    public long MarketCap { get; set; }
    public List<CommentDto> Comments { get; set; } = [];
}