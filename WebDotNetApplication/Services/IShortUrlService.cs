using WebDotNetApplication.Models;

namespace WebDotNetApplication.Services;

public interface IShortUrlService
{
    UrlDataDto AddUrlData(UrlDataDto urlDataDto, string requestScheme, string requestHost);

    UrlDataDto GetUrlData(string shortUrl);

    bool CheckUserAgent(string headersUserAgent);

    bool CheckIfUrlExists(UrlDataDto urlDataDto);
}