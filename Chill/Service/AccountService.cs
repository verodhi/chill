using Chill.Model;
using Chill.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chill.Service
{
    public class AccountService : IAccountService
    {
        private IAccountRepository _repository;
        private ICurrencyToDollarConverter _converter;
        private IAccountRepository @object;

        public AccountService(IAccountRepository repository, ICurrencyToDollarConverter converter)
        {
            _repository = repository;
            _converter = converter;
        }

        public AccountService(IAccountRepository @object)
        {
            this.@object = @object;
        }

        public AccountResultVM Get(int accountNumber)
        {
            var currentAccount = _repository.Get(accountNumber);
            if (currentAccount == null)
            {
                return new AccountResultVM(accountNumber, false, 0, null, "Account could not be found");
            }
            var accountResultVM = new AccountResultVM(currentAccount.AccountNumber, true, currentAccount.Balance, currentAccount.Currency, "Current account information.");
            return accountResultVM;
        }

        public async Task<AccountResultVM> Deposit(AccountDepositWithdrawDTO accountDepositWithdrawDTO)
        {
            try
            {
                var currentAccount = _repository.Get(accountDepositWithdrawDTO.AccountNumber);
                if (currentAccount == null)
                {
                    return new AccountResultVM(accountDepositWithdrawDTO.AccountNumber, false, 0, null, "Account could not be found");
                }
                var dollarAmount = _converter.ConvertToDollar(accountDepositWithdrawDTO.Currency, accountDepositWithdrawDTO.Amount);
                currentAccount.Balance += dollarAmount;
                await _repository.SaveAllAsync();
                return new AccountResultVM(currentAccount.AccountNumber, true, currentAccount.Balance, currentAccount.Currency, "Amount deposited, here is current account info.");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new AccountResultVM(accountDepositWithdrawDTO.AccountNumber, false, accountDepositWithdrawDTO.Amount, accountDepositWithdrawDTO.Currency, "Deposit failed, changes were made to the account before the deposit was made.");
            }
        }

        public async Task<AccountResultVM> Withdraw(AccountDepositWithdrawDTO accountDepositWithdrawDTO)
        {
            try
            {
                var currentAccount = _repository.Get(accountDepositWithdrawDTO.AccountNumber);
                if (currentAccount == null)
                {
                    return new AccountResultVM(accountDepositWithdrawDTO.AccountNumber, false, 0, null, "Account could not be found");
                }
                var dollarAmount = _converter.ConvertToDollar(accountDepositWithdrawDTO.Currency, accountDepositWithdrawDTO.Amount);
                if (currentAccount.Balance < dollarAmount)
                {
                    // Assuming we don't want to let the user know how much they have in the account.
                    return new AccountResultVM(accountDepositWithdrawDTO.AccountNumber, false, 0, null, "You don't have enough money in your account.");
                }
                currentAccount.Balance -= dollarAmount;
                await _repository.SaveAllAsync();
                return new AccountResultVM(currentAccount.AccountNumber, true, currentAccount.Balance, currentAccount.Currency, "Amount withdrawn, here is current account info.");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new AccountResultVM(accountDepositWithdrawDTO.AccountNumber, false, accountDepositWithdrawDTO.Amount, accountDepositWithdrawDTO.Currency, $"Withdraw failed, changes were made to the account before the withdraw could be made. {ex.Message}");
            }
        }
    }
}
