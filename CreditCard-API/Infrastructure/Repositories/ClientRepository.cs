using Core.Entities;
using Core.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext dbContext)
            :base(dbContext)
        {}

        public override async Task<IReadOnlyList<Client>> GetAllAsync()
        {
            return await _dbContext.Clients
                .Include(x => x.CreditCards)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public override async Task<Client> GetByIdAsync(int id)
        {
            return await _dbContext.Clients
                .AsNoTracking()
                .Include(x => x.CreditCards)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
