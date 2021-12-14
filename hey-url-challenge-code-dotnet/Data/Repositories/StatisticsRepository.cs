using hey_url_challenge_code_dotnet.Data.Repositories.Interfaces;
using hey_url_challenge_code_dotnet.Models;
using HeyUrlChallengeCodeDotnet.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Data.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<UrlStatistics> DbSet;

        public StatisticsRepository(ApplicationContext context)
        {
            _context = context;
            DbSet = _context.Set<UrlStatistics>();
        }
        public async Task<IEnumerable<UrlStatistics>> ReadByUrlIdAsync(Guid Id)
        {
            return await _context.Statistics.Where(x => x.Url.Id == Id).ToListAsync();

        }
        public async Task<IEnumerable<UrlStatistics>> ReadAllAsync()
        {
            return await DbSet.ToListAsync();

        }
        public async Task<UrlStatistics> AddAsync(UrlStatistics urlStatistics)
        {
            var entryEntity = await DbSet.AddAsync(urlStatistics);
            await _context.SaveChangesAsync();
            return entryEntity.Entity;

        }

    }
}
