using Chill.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chill.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountContext _accountContext;
        public AccountRepository(AccountContext accountContext)
        {
            _accountContext = accountContext;
        }

        public Account Get(int accountNumber)
        {
            return _accountContext.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        }

        public async Task<bool> SaveAllAsync()
        {
            return (await _accountContext.SaveChangesAsync()) > 0;
        }
    }
}
