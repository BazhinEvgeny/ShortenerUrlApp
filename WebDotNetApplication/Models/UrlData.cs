using System.ComponentModel.DataAnnotations;

namespace WebDotNetApplication.Models;

public class UrlData
{
    public int Id { get; set; }

    [Required]
    public string Url { get; set; }

    [Required]
    public DateTime ShorteningDateTime { get; set; }
}