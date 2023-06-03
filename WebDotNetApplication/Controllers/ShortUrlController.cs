using Microsoft.AspNetCore.Mvc;
using WebDotNetApplication.Models;
using WebDotNetApplication.Services;

namespace WebDotNetApplication.Controllers;

[ApiController]
public class ShortUrlController : ControllerBase
{ 
    private readonly IShortUrlService _shortUrlService;

    public ShortUrlController(IShortUrlService shortUrlService)
    {
        _shortUrlService = shortUrlService;
    }
    
    [HttpPost("short")]
    public async Task<IActionResult> Short(UrlDataDto request)
    {
        if (!Uri.TryCreate(request.Url, UriKind.Absolute, out var inputUri))
        {
            throw new Exception("URL is invalid.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (_shortUrlService.CheckIfUrlExists(request))
        {
            ModelState.AddModelError("Url", "Url is already shortened");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = _shortUrlService.AddUrlData(request, this.Request.Scheme, this.Request.Host.ToString());

        return Created("shortUrl", result);
    }
    
    [HttpGet("{shortUrl}")]
    public IActionResult GetUrl(string shortUrl)
    {
        if (String.IsNullOrEmpty(shortUrl))
            return BadRequest();

        var urlDataDto = _shortUrlService.GetUrlData(shortUrl);

        if (urlDataDto == null)
            return NotFound();

        var userAgentBrowser = _shortUrlService.CheckUserAgent(this.Request.Headers["User-Agent"].ToString());

        if (userAgentBrowser)
            return RedirectPermanent(urlDataDto.Url);
            
        return Ok(urlDataDto);
    }
}