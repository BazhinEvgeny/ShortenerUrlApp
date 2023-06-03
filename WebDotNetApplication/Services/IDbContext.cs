
using WebDotNetApplication.Models;

namespace WebDotNetApplication.Services;

public interface IDbContext
{
    int AddUrl(UrlData urlData);

    UrlData GetUrl(int id);

    bool CheckIfUrlExists(string longUrl);
}