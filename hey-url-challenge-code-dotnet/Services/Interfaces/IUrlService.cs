using hey_url_challenge_code_dotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Services.Interfaces
{
    public interface IUrlService
    {
        string GenerateCode();
        bool IsValidUrl(string url);
        Task<IEnumerable<Url>> ReaAllAsync();
        Task<Url> SaveUrl(string shortUrl, string baseUrl, string originalUrl);
        Task<Url> ReadByCode(string code);
        Task<Url> UpdateUrl(Url url);
    }
}
