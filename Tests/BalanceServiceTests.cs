using Bank.Factory;
using Bank.Factory.Processor;
using Bank.Models;
using Bank.Models.Request;
using Bank.Models.Response;
using Bank.Repository;
using Bank.Services;
using Microsoft.Extensions.Logging;
using Moq;
using FluentAssertions;

namespace Tests
{
    public class BalanceServiceTests
    {
        private readonly Mock<IAccountRepository> _accountRepositoryMock;

        private readonly Mock<ILogger<BalanceService>> _loggerMock;

        private readonly BalanceService _balanceService;

        public BalanceServiceTests()
        {
            _accountRepositoryMock = new Mock<IAccountRepository>();
            _loggerMock = new Mock<ILogger<BalanceService>>();
            _balanceService = new BalanceService(_accountRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public void GetBalance_ShouldReturnBalance_WhenAccountIdIsValid()
        {
            // Arrange
            var accountId = "123";
            var expectedBalance = 100m;
            _accountRepositoryMock.Setup(repo => repo.GetBalance(accountId)).Returns(expectedBalance);

            // Act
            var result = _balanceService.GetBalance(accountId);

            // Assert
            expectedBalance.Should().Be(result);
        }

        [Test]
        public void Movement_ShouldReturnMovement_WhenTransactionIsValid()
        {
            // Arrange
            var transaction = new Transaction { Type = MovementType.Deposit, Amount = 50m, Destination = "123" };
            var expectedMovement = new Movement { };
            var processorMock = new Mock<ITransactionProcessor>();
            processorMock.Setup(p => p.Process(transaction, _accountRepositoryMock.Object)).Returns(expectedMovement);
            TransactionProcessorFactory.CreateProcessor(transaction.Type);

            // Act
            var result = _balanceService.Movement(transaction);

            // Assert
            expectedMovement.Should().Be(result);
        }

        [Test]
        public void Reset_ShouldCallResetOnRepository_AndLogInformation()
        {
            // Act
            _balanceService.Reset();

            // Assert
            _accountRepositoryMock.Verify(repo => repo.Reset(), Times.Once);
            _loggerMock.Verify(logger => logger.LogInformation("Resetting all accounts"), Times.Once);
        }
    }
}