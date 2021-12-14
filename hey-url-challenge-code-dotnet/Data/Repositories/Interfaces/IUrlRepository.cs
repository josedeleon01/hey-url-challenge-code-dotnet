using hey_url_challenge_code_dotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Data.Repositories.Interfaces
{
    public interface IUrlRepository
    {
        Task<IEnumerable<Url>> ReadAllAsync();
        Task<Url> AddAsync(Url url);
        Task<Url> ReadByCode(string code);
        Task<Url> UpdateAsync(Url url);

    }
}
