using GatewayApiClient;
using GatewayApiClient.DataContracts;
using GatewayApiClient.DataContracts.EnumTypes;
using GatewayApiClient.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TicketsApi.Domain.Exceptions;
using TicketsApi.Domain.Interfaces;
using TicketsApi.Domain.Interfaces.Repository;
using TicketsApi.Domain.Models;

namespace TicketsApi.Domain.CreditCardsCompanies
{
    public class MundipaggCompany : ICreditCardCompany
    {
        private readonly Guid _merchantKey = Guid.Parse("c8c45851-3b2e-49ce-887b-163e15cf4d18");
        private readonly Uri _hostUri = new Uri("https://sandbox.mundipaggone.com");

        private ICreditCardTokenService _creditCardTokenService;
        
        public MundipaggCompany(ICreditCardTokenService creditCardTokenService)
        {
            _creditCardTokenService = creditCardTokenService;
        }

        public void MakeTransaction(UserCreditCard userCreditCardInfo)
        {
            try
            {
                var transaction = CreateCreditCardTransaction(userCreditCardInfo);

                Collection<CreditCardTransaction> CreditCardTransactionCollection =
                    new Collection<CreditCardTransaction>(new CreditCardTransaction[] { transaction });

                var createSaleRequest = CreateSaleRequest(CreditCardTransactionCollection);

                var serviceClient = new GatewayServiceClient(_merchantKey, _hostUri);

                var httpResponse = serviceClient.Sale.Create(createSaleRequest);

                // API response code
                ResponseProcess(userCreditCardInfo, httpResponse);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public void MakeTransactionWithCreditCardToken(UserCreditCard userCreditCardInfo)
        {
            Guid instantBuyKey = GetInstantBuyKeyByUserId(userCreditCardInfo.UserId);

            try
            {
                var transaction = CreateCreditCardTransactionWithToken(userCreditCardInfo, instantBuyKey);

                Collection<CreditCardTransaction> CreditCardTransactionCollection =
                    new Collection<CreditCardTransaction>(new CreditCardTransaction[] { transaction });

                var createSaleRequest = CreateSaleRequest(CreditCardTransactionCollection);

                var serviceClient = new GatewayServiceClient(_merchantKey, _hostUri);

                var httpResponse = serviceClient.Sale.Create(createSaleRequest);

                // API response code
                ResponseProcess(userCreditCardInfo, httpResponse);
            }
            catch (Exception)
            {
                throw;
            }

        }


        //-------------------------

        private Guid GetInstantBuyKeyByUserId(int userId)
        {
            Guid instantBuyKey = _creditCardTokenService.GetCreditCardTokenByUserId(userId).CreditCardTokenId;

            // Cria o cliente para obter os dados do cartão.
            var client = new GatewayServiceClient(_merchantKey, _hostUri);

            // Obtém os dados do cartão de crédito no gateway.
            var httpResponse = client.CreditCard.GetInstantBuyData(instantBuyKey);

            if (httpResponse.HttpStatusCode == HttpStatusCode.OK
                && httpResponse.Response.CreditCardDataCollection.Any() == true)
            {
                return instantBuyKey;
            }

            throw new Exception("Erro ao buscar o token do cartão de crédito");
        }

        private CreditCardTransaction CreateCreditCardTransaction(UserCreditCard userCreditCardInfo)
        {
            var transaction = new CreditCardTransaction()
            {
                AmountInCents = userCreditCardInfo.AmountInCents  * 100,
                CreditCard = new CreditCard()
                {
                    CreditCardNumber = userCreditCardInfo.CreditCardNumber,
                    CreditCardBrand = userCreditCardInfo.CreditCardBrand,
                    ExpMonth = userCreditCardInfo.ExpMonth,
                    ExpYear = userCreditCardInfo.ExpYear,
                    SecurityCode = userCreditCardInfo.SecurityCode,
                    HolderName = userCreditCardInfo.HolderName
                },
                InstallmentCount = userCreditCardInfo.InstallmentCount
            };

            return transaction;
        }

        private CreditCardTransaction CreateCreditCardTransactionWithToken(UserCreditCard userCreditCardInfo, Guid instantBuyKey)
        {
            var transaction = new CreditCardTransaction()
            {
                AmountInCents = userCreditCardInfo.AmountInCents * 100,
                CreditCard = new CreditCard()
                {
                    InstantBuyKey = instantBuyKey
                },
                InstallmentCount = userCreditCardInfo.InstallmentCount
            };

            return transaction;
        }


        private CreateSaleRequest CreateSaleRequest(Collection<CreditCardTransaction> CreditCardTransactionCollection)
        {
            var createSaleRequest = new CreateSaleRequest()
            {
                // Add the transaction in the request.
                CreditCardTransactionCollection = CreditCardTransactionCollection,
                Order = new Order()
                {
                    OrderReference = Guid.NewGuid().ToString()
                }
            };

            return createSaleRequest;
        }

        private void ResponseProcess(UserCreditCard userCreditCardInfo, HttpResponse<CreateSaleResponse> httpResponse)
        {
            var createSaleResponse = httpResponse.Response;
            if (httpResponse.HttpStatusCode == HttpStatusCode.Created)
            {
                foreach (var creditCardTransaction in createSaleResponse.CreditCardTransactionResultCollection)
                {
                    if (creditCardTransaction.CreditCardTransactionStatus == CreditCardTransactionStatusEnum.NotAuthorized)
                    {
                        throw new TransactionNotAuthorizedException();
                    }
                        
                    if (userCreditCardInfo.IsSavingCreditCardInToken)
                    {
                        Guid instantBuyKey = creditCardTransaction.CreditCard.InstantBuyKey;
                        SaveTokenCreditCard(instantBuyKey, userCreditCardInfo.UserId);
                    }
                }
            }
            else
            {
                if (createSaleResponse.ErrorReport != null)
                {
                    foreach (ErrorItem errorItem in createSaleResponse.ErrorReport.ErrorItemCollection)
                    {
                        throw new ErrorTransactionException(errorItem.Description);
                    }
                }
            }
        }

        

        private void SaveTokenCreditCard(Guid InstantBuyKey, int userId)
        {
            CreditCardToken token = new CreditCardToken
            {
                CreditCardTokenId = InstantBuyKey,
                UserId = userId
            };

            _creditCardTokenService.AddCreditCardToken(token);
        }
    }
}
