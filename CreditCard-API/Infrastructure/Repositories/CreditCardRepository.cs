using Core.Entities;
using Core.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CreditCardRepository : BaseRepository<CreditCard>,  ICreditCardRepository
    {
        public CreditCardRepository(ApplicationDbContext dbContext)
            :base(dbContext) {}
    }
}
