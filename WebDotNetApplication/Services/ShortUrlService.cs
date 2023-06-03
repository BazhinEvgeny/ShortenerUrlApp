using WebDotNetApplication.Models;

namespace WebDotNetApplication.Services;

public class ShortUrlService : IShortUrlService
{
    private IDbContext _dbContext;
    private IUrlHelper _urlHelper;

    public ShortUrlService(IDbContext dbContext, IUrlHelper urlHelper)
    {
        _dbContext = dbContext;
        _urlHelper = urlHelper;
    }

    public UrlDataDto GetUrlData(string shortUrl)
    {
        var id = _urlHelper.GetId(shortUrl);
        var urlData = _dbContext.GetUrl(id);
        var urlDataDto = new UrlDataDto()
        {
            Url = urlData.Url
        };
        return urlDataDto;
    }

    public UrlDataDto AddUrlData(UrlDataDto urlDataDto, string requestScheme, string requestHost)
    {
        var newEntry = new UrlData
        {
            Url = urlDataDto.Url,
            ShorteningDateTime = DateTime.Now.Date
        };

        var id = _dbContext.AddUrl(newEntry);
        UrlDataDto responseUrlDataDto = new UrlDataDto() { Url = $"{requestScheme}://{requestHost}/{_urlHelper.GetShortUrl(id)}" };
        
        return responseUrlDataDto;
    }

    public bool CheckUserAgent(string headersUserAgent)
    {
        return headersUserAgent.Contains("Mozilla") ||
               headersUserAgent.Contains("AppleWebKit") ||
               headersUserAgent.Contains("Chrome") ||
               headersUserAgent.Contains("Safari") ||
               headersUserAgent.Contains("Edg");
    }

    public bool CheckIfUrlExists(UrlDataDto urlDataDto)
    {
        bool urlExists = false;
        urlExists = _dbContext.CheckIfUrlExists(urlDataDto.Url);

        return urlExists;
    }
}