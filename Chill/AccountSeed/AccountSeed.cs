using Chill.Model;
using Chill.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chill.AccountSeed
{
    public class AccountSeed
    {
        private readonly AccountContext _context;

        public AccountSeed(AccountContext context)
        {
            _context = context;
        }

        public async Task InitializeDummyData()
        {
            if (!_context.Accounts.Any())
            {
                _context.AddRange(_account);
                await _context.SaveChangesAsync();
            }
        }

        private readonly List<Account> _account = new List<Account>
        {
            new Account()
            {
                Balance= 1000,
                Currency = "United States Dollar"
            },
            new Account()
            {
                Balance= 2000,
                Currency = "United States Dollar"
            }
        };
    }
}
