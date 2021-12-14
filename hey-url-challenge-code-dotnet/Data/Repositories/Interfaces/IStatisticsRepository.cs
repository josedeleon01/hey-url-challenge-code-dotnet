using hey_url_challenge_code_dotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Data.Repositories.Interfaces
{
    public interface IStatisticsRepository
    {
        Task<IEnumerable<UrlStatistics>> ReadByUrlIdAsync(Guid Id);
        Task<IEnumerable<UrlStatistics>> ReadAllAsync();
        Task<UrlStatistics> AddAsync(UrlStatistics urlStatistics);

    }
}
