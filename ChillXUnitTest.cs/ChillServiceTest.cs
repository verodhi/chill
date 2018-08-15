using AutoFixture;
using Chill.Model;
using Chill.Repository;
using Chill.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ChillXUnitTest.cs
{
    public class ChillServiceTest
    {
        private readonly AccountService service;
        private readonly Mock<IAccountRepository> repository;
        private readonly Mock<ICurrencyToDollarConverter> converter;
        private Fixture fixture;

        public ChillServiceTest()
        {
            repository = new Mock<IAccountRepository>();
            converter = new Mock<ICurrencyToDollarConverter>();
            fixture = new Fixture();
            service = new AccountService(repository.Object, converter.Object);
        }

        [Fact]
        public void Get_TakesAccountNumber_Returns_AccountInfo()
        {
            //Arrange
            var accountNumber = 1;
            var account = fixture.Create<Account>();
            repository.Setup(s => s.Get(accountNumber)).Returns(account).Verifiable();
            //Act
            var accountInfoResult = service.Get(accountNumber);
            //Assert
            Assert.NotNull(accountInfoResult);
            Assert.IsType<AccountResultVM>(accountInfoResult);
            Assert.Equal(accountInfoResult.Balance, account.Balance);
            Assert.Equal(accountInfoResult.Currency, account.Currency);
            Assert.Equal(accountInfoResult.AccountNumber, account.AccountNumber);
            Assert.True(accountInfoResult.Successful);
            repository.Verify(s => s.Get(accountNumber), Times.Once);
        }

        [Fact]
        public void Get_TakesAccountNumber_Returns_AccountInfo_Fails()
        {
            //Arrange
            var accountNumber = 1;
            Account account = null;
            repository.Setup(s => s.Get(accountNumber)).Returns(account).Verifiable();
            //Act
            var accountInfoResult = service.Get(accountNumber);
            //Assert
            Assert.NotNull(accountInfoResult);
            Assert.IsType<AccountResultVM>(accountInfoResult);
            Assert.Equal(0, accountInfoResult.Balance);
            Assert.Null(accountInfoResult.Currency);
            Assert.False(accountInfoResult.Successful);
            repository.Verify(s => s.Get(accountNumber), Times.Once);
        }
    }
}
