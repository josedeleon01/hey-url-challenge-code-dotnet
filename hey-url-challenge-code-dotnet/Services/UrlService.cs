using hey_url_challenge_code_dotnet.Data.Repositories;
using hey_url_challenge_code_dotnet.Data.Repositories.Interfaces;
using hey_url_challenge_code_dotnet.Models;
using hey_url_challenge_code_dotnet.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Services
{
    public class UrlService : IUrlService
    {
        private readonly IUrlRepository _UrlRepository;

        public UrlService(IUrlRepository urlRepository)
        {
            _UrlRepository = urlRepository;

        }

        public string GenerateCode()
        {
            return RandomString(5);

        }
        public bool IsValidUrl(string url)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return result;

        }

        public async Task<IEnumerable<Url>> ReaAllAsync()
        {
            return await _UrlRepository.ReadAllAsync();

        }
        public async Task<Url> SaveUrl(string shortUrl, string baseUrl, string originalUrl)
        {
            return await _UrlRepository.AddAsync(new Url
            {
                Id = Guid.NewGuid(),
                Count = 0,
                OriginalUrl = originalUrl,
                ShortUrl = shortUrl,
                Created = DateTime.Now
            });

        }

        public async Task<Url> ReadByCode(string code)
        {
            return await _UrlRepository.ReadByCode(code);

        }

        public async Task<Url> UpdateUrl(Url url)
        {
            return await _UrlRepository.UpdateAsync(url);

        }

        private string RandomString(int length)
        {
            var base64Guid = Guid.NewGuid().ToString();
            var uniqueId = Regex.Replace(base64Guid, @"[^a-zA-Z]", string.Empty);

            return uniqueId.Substring(0, length).ToUpper();
        }
    }
}
