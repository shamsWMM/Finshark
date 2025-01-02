using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace api.Dtos;

public class CommentDto
{
    [Required]
    public string Title { get; set; } = String.Empty;

    [Required]
    public string Content { get; set; } = String.Empty;
}