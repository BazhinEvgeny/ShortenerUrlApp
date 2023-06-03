namespace WebDotNetApplication.Services;

public interface IUrlHelper
{
    string GetShortUrl(int id);

    int GetId(string shortUrl);
}