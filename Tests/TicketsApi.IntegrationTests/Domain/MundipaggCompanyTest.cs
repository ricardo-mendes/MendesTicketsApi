using GatewayApiClient.DataContracts.EnumTypes;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsApi.Domain.CreditCardsCompanies;
using TicketsApi.Domain.Exceptions;
using TicketsApi.Domain.Interfaces;
using TicketsApi.Domain.Interfaces.Repository;
using TicketsApi.Domain.Models;
using TicketsApi.Domain.Services;
using Xunit;

namespace TicketsApi.IntegrationTests.Domain
{
    public class MundipaggCompanyTest
    {

        [Fact]
        public void When_Making_Transaction_Should_Not_Throw_Exception()
        {
            //Arrange 
            UserCreditCard userCreditCard = new UserCreditCard
            {
                UserId = 1,
                AmountInCents = 1000,
                IsSavingCreditCardInToken = false,
                CreditCardNumber = "4111111111111111",
                CreditCardBrand = CreditCardBrandEnum.Visa,
                ExpMonth = 10,
                ExpYear = 22,
                HolderName = "LUKE SKYWALKER",
                SecurityCode = "123",
                InstallmentCount = 1
            };

            var creditCardTokenRepositoryMock = new Mock<ICreditCardTokenRepository>();

            ICreditCardTokenService creditCardTokenService = new CreditCardTokenService(creditCardTokenRepositoryMock.Object);
            ICreditCardCompany mundipaggCompany = new MundipaggCompany(creditCardTokenService);

            //Act
            //Assert       
            mundipaggCompany.MakeTransaction(userCreditCard);
        }

        [Fact]
        public void When_Making_Transaction_With_Credit_Card_Token_Should_Not_Throw_Exception()
        {
            //Arrange 
            UserCreditCard userCreditCard = new UserCreditCard
            {
                UserId = 1,
                AmountInCents = 1000,
                IsSavingCreditCardInToken = true,
                InstallmentCount = 1
            };

            Guid instantBuyKey = new Guid("33B92AFF-4F90-4F6A-A0FB-18FA50DBA364");

            CreditCardToken creditCardToken = new CreditCardToken
            {
                UserId = 1,
                CreditCardTokenId = instantBuyKey
            };

            var creditCardTokenRepositoryMock = new Mock<ICreditCardTokenRepository>();

            creditCardTokenRepositoryMock.Setup(x => x.GetCreditCardTokenByUserId(1)).Returns(creditCardToken);

            ICreditCardTokenService creditCardTokenService = new CreditCardTokenService(creditCardTokenRepositoryMock.Object);
            ICreditCardCompany mundipaggCompany = new MundipaggCompany(creditCardTokenService);

            //Act
            //Assert            
            mundipaggCompany.MakeTransactionWithCreditCardToken(userCreditCard);
        }

        [Fact]
        public void When_Making_Transaction_With_Token_And_User_Does_Not_Have_A_Credit_Card_Saved_Should_Throw_CreditCardTokenException()
        {
            //Arrange 
            UserCreditCard userCreditCard = new UserCreditCard
            {
                UserId = 1
            };

            CreditCardToken creditCardToken = null;

            var creditCardTokenRepositoryMock = new Mock<ICreditCardTokenRepository>();
            
            creditCardTokenRepositoryMock.Setup(x => x.GetCreditCardTokenByUserId(1)).Returns(creditCardToken);

            ICreditCardTokenService creditCardTokenService = new CreditCardTokenService(creditCardTokenRepositoryMock.Object);
            ICreditCardCompany mundipaggCompany = new MundipaggCompany(creditCardTokenService);

            //Act
            //Assert            
            Assert.Throws<CreditCardTokenException>(() => mundipaggCompany.MakeTransactionWithCreditCardToken(userCreditCard));
        }

        [Fact]
        public void When_Making_Transaction_And_Credit_Card_Unauthorized_The_Sale_Should_Throw_TransactionNotAuthorizedException()
        {
            //Arrange 
            UserCreditCard userCreditCard = new UserCreditCard
            {
                UserId = 1,
                AmountInCents = 1100,
                IsSavingCreditCardInToken = false,
                CreditCardNumber = "4111111111111111",
                CreditCardBrand = CreditCardBrandEnum.Visa,
                ExpMonth = 10,
                ExpYear = 22,
                HolderName = "LUKE SKYWALKER",
                SecurityCode = "123",
                InstallmentCount = 1
            };

            var creditCardTokenRepositoryMock = new Mock<ICreditCardTokenRepository>();

            ICreditCardTokenService creditCardTokenService = new CreditCardTokenService(creditCardTokenRepositoryMock.Object);
            ICreditCardCompany mundipaggCompany = new MundipaggCompany(creditCardTokenService);

            //Act
            //Assert            
            Assert.Throws<TransactionNotAuthorizedException>(() => mundipaggCompany.MakeTransaction(userCreditCard));
        }

        [Fact]
        public void When_Making_Transaction_With_Token_And_Credit_Card_Unauthorized_The_Sale_Should_Throw_TransactionNotAuthorizedException()
        {
            //Arrange 
            UserCreditCard userCreditCard = new UserCreditCard
            {
                UserId = 1,
                AmountInCents = 1100,
                IsSavingCreditCardInToken = true,
                InstallmentCount = 1
            };

            Guid instantBuyKey = new Guid("33B92AFF-4F90-4F6A-A0FB-18FA50DBA364");

            CreditCardToken creditCardToken = new CreditCardToken
            {
                UserId = 1,
                CreditCardTokenId = instantBuyKey
            };

            var creditCardTokenRepositoryMock = new Mock<ICreditCardTokenRepository>();

            creditCardTokenRepositoryMock.Setup(x => x.GetCreditCardTokenByUserId(1)).Returns(creditCardToken);

            ICreditCardTokenService creditCardTokenService = new CreditCardTokenService(creditCardTokenRepositoryMock.Object);
            ICreditCardCompany mundipaggCompany = new MundipaggCompany(creditCardTokenService);

            //Act
            //Assert            
            Assert.Throws<TransactionNotAuthorizedException>(() => mundipaggCompany.MakeTransactionWithCreditCardToken(userCreditCard));

        }



    }
}
