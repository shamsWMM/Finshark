using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using static api.Helpers.ValidationHelper;
using System.Text.Json.Serialization;

namespace api.Dtos;

public class CommentDto
{
    [JsonIgnore]
    public int Id { get; set; }
    [JsonIgnore]
    public string Username { get; set; } = string.Empty;
    [Required]
    [MinLength(1, ErrorMessage = TextTooShort)]
    [MaxLength(50, ErrorMessage = TextTooLong)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MinLength(1, ErrorMessage = TextTooShort)]
    [MaxLength(500, ErrorMessage = TextTooLong)]
    public string Content { get; set; } = string.Empty;
}