using System.ComponentModel.DataAnnotations;

namespace WebDotNetApplication.Models;

public class UrlDataDto
{
    [Required]
    public string Url { get; set; }
}