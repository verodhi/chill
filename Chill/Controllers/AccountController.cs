using Chill.Model;
using Chill.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Chill.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private IAccountService _service;
        private ILogger<AccountController> _logger;

        public AccountController(IAccountService service, ILogger<AccountController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Get Account by AccountNumber
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        [HttpGet("{accountNumber}")]
        public ActionResult<AccountResultVM> Get(int accountNumber)
        {
            try
            {
                var account = _service.Get(accountNumber);
                return new OkObjectResult(new AccountResultVM(account.AccountNumber, true, account.Balance, account.Currency, account.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error while accessing account info.");
                return BadRequest(Json("Internal server error."));
            }

        }

        /// <summary>
        /// Deposit monye by account number.
        /// </summary>
        /// <param name="accountDepositWithdrawDTO"></param>
        /// <returns></returns>
        [HttpPost("Deposit")]
        public async Task<ActionResult<AccountResultVM>> Deposit([FromBody] AccountDepositWithdrawDTO accountDepositWithdrawDTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(e => e.Count > 0)
                           .ToList().ToString();
                return BadRequest(new AccountResultVM() { Message = $"You are sending invalid model data. {errors}" });
            }
            try
            {
                var result = await _service.Deposit(accountDepositWithdrawDTO);
                if (result.Successful)
                {
                    _logger.LogInformation("Deposited", accountDepositWithdrawDTO);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error while depositing money in account.");
                return BadRequest(Json("Internal server error."));
            }
        }

        /// <summary>
        /// Withdraw money by account number.
        /// </summary>
        /// <param name="accountDepositWithdrawDTO"></param>
        /// <returns></returns>
        [HttpPost("Withdraw")]
        public async Task<ActionResult<AccountResultVM>> Withdraw([FromBody] AccountDepositWithdrawDTO accountDepositWithdrawDTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(e => e.Count > 0)
                           .ToList().ToString();
                return BadRequest(new AccountResultVM() { Message = $"You are sending invalid model data. {errors}" });
            }
            try
            {
                var result = await _service.Withdraw(accountDepositWithdrawDTO);
                if (result.Successful)
                {
                    _logger.LogInformation("Withdrawn", accountDepositWithdrawDTO);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error while withdrawing money in account.");
                return BadRequest(Json("Internal server error."));
            }
        }
    }
}