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
    public class UrlRepository : IUrlRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Url> DbSet;
        public UrlRepository(ApplicationContext context)
        {
            _context = context;
            DbSet = _context.Set<Url>();
        }

        public async Task<IEnumerable<Url>> ReadAllAsync()
        {
            return await DbSet.ToListAsync();
        }
        public async Task<Url> AddAsync(Url url)
        {
            var entryEntity = await DbSet.AddAsync(url);
            await _context.SaveChangesAsync();
            return entryEntity.Entity;
        }

        public async Task<Url> ReadByCode(string code)
        {
            return await _context.Urls.FirstOrDefaultAsync(x => x.ShortUrl == code);

        }

        public async Task<Url> UpdateAsync(Url url)
        {
            var entryEntity = DbSet.Update(url);
            await _context.SaveChangesAsync();
            return entryEntity.Entity;
        }
    }
}
