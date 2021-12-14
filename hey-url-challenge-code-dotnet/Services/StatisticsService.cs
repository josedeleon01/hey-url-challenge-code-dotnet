using hey_url_challenge_code_dotnet.Data.Repositories.Interfaces;
using hey_url_challenge_code_dotnet.Models;
using hey_url_challenge_code_dotnet.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepository _StatisticsRepository;

        public StatisticsService(IStatisticsRepository statisticsRepository)
        {
            _StatisticsRepository = statisticsRepository;

        }
        public async Task<IEnumerable<UrlStatistics>> ReadByUrlIdAsync(Guid Id)
        {
            return await _StatisticsRepository.ReadByUrlIdAsync(Id);

        }
        public async Task<IEnumerable<UrlStatistics>> ReadAllAsync()
        {
            return await _StatisticsRepository.ReadAllAsync();
        
        }
        public async Task<UrlStatistics> SaveAsync(UrlStatistics urlStatistics)
        {
            return await _StatisticsRepository.AddAsync(urlStatistics);
        
        }
    }
}
