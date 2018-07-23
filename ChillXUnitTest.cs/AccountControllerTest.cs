using Chill.Controllers;
using Chill.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Chill.Model;
using System.Threading.Tasks;

namespace ChillXUnit.Test
{
    public class AccountControllerTest
    {
        private readonly AccountController controller;
        private Mock<ILogger<AccountController>> logger;
        private readonly Mock<IAccountService> service;
        private Fixture fixture;

        public AccountControllerTest()
        {
            service = new Mock<IAccountService>();
            logger = new Mock<ILogger<AccountController>>();
            controller = new AccountController(service.Object, logger.Object);
            fixture = new Fixture();
        }

        [Fact]
        public void Account_Get_TakesAnAcountNumber_HandelsException()
        {
            //Arrange
            var accountNumber = 1;
            var exception = fixture.Create<Exception>();
            service.Setup(s => s.Get(It.IsAny<int>())).Throws(exception).Verifiable();
            //Act
            var result = controller.Get(accountNumber);
            //Assert
            Assert.NotNull(result);
            service.Verify(s => s.Get(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Account_Get_TakesAnAcountNumber_ReturnsAccount()
        {
            //Arrange
            var accountNumber = 1;
            var accountInfo = fixture.Create<AccountResultVM>();
            service.Setup(s => s.Get(accountNumber)).Returns(accountInfo).Verifiable();
            //Act
            var actionResult = controller.Get(accountNumber);
            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<AccountResultVM>>(actionResult);
            service.Verify(s => s.Get(accountNumber), Times.Once);
        }

        [Fact]
        public async Task Account_Deposit_TakesDepositDTO_HandelsException()
        {
            //Arrange
            var depositDTO = fixture.Create<AccountDepositWithdrawDTO>();
            var exception = fixture.Create<Exception>();
            service.Setup(s => s.Deposit(depositDTO)).Throws(exception).Verifiable();
            //Act
            var actionResult = await controller.Deposit(depositDTO);
            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<AccountResultVM>>(actionResult);
            service.Verify(s => s.Deposit(depositDTO), Times.Once);
        }

        [Fact]
        public async Task Account_Deposit_TakesDepositDTO_ReturnsAccount()
        {
            //Arrange
            var depositDTO = fixture.Create<AccountDepositWithdrawDTO>();
            var accountInfo = fixture.Create<Task<AccountResultVM>>();
            service.Setup(s => s.Deposit(depositDTO)).Returns(accountInfo).Verifiable();
            //Act
            var actionResult = await controller.Deposit(depositDTO);
            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<AccountResultVM>>(actionResult);
            service.Verify(s => s.Deposit(depositDTO), Times.Once);
        }

        [Fact]
        public async Task Account_Withdraw_TakesDepositDTO_HandelsException()
        {
            //Arrange
            var depositDTO = fixture.Create<AccountDepositWithdrawDTO>();
            var exception = fixture.Create<Exception>();
            service.Setup(s => s.Withdraw(depositDTO)).Throws(exception).Verifiable();
            //Act
            var actionResult = await controller.Withdraw(depositDTO);
            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<AccountResultVM>>(actionResult);
            service.Verify(s => s.Withdraw(depositDTO), Times.Once);
        }

        [Fact]
        public async Task Account_Withdraw_TakesDepositDTO_ReturnsAccount()
        {
            //Arrange
            var depositDTO = fixture.Create<AccountDepositWithdrawDTO>();
            var accountInfo = fixture.Create<Task<AccountResultVM>>();
            service.Setup(s => s.Withdraw(depositDTO)).Returns(accountInfo).Verifiable();
            //Act
            var actionResult = await controller.Withdraw(depositDTO);
            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<AccountResultVM>>(actionResult);
            service.Verify(s => s.Withdraw(depositDTO), Times.Once);
        }
    }
}
