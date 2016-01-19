using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TicketsApi.Domain.CreditCardsCompanies;
using TicketsApi.Domain.Exceptions;
using TicketsApi.Domain.Interfaces;
using TicketsApi.Domain.Models;
using TicketsApi.Domain.Services;
using TicketsApi.EfData.Context;
using TicketsApi.EfData.Repositories;
using TicketsApi.Mapper;
using TicketsApi.ViewModels;

namespace TicketsApi.Web.Controllers
{
    public class PaymentController : ApiController
    {
        private ICreditCardCompany _cardCompany;
        private IUserService _userService;
        private ITransactionCreditCardErrorService _transactionCreditCardErrorService;
        private CreditCardMapper _creditCardMapper;

        public PaymentController()
        {
            TicketsContext context = new TicketsContext();
            _userService = new UserService(new UserRepository(context));
            _transactionCreditCardErrorService = new TransactionCreditCardErrorService(new TransactionCreditCardErrorRepository(context));
            _cardCompany = new MundipaggCompany(new CreditCardTokenService(new CreditCardTokenRepository(context)));
            _creditCardMapper = new CreditCardMapper();
        }

        public HttpResponseMessage Post([FromBody] UserCreditCardViewModel creditCardViewModel)
        {
            UserCreditCard userCreditCard = _creditCardMapper.MapToCreditCardInfo(creditCardViewModel);

            var parameters = Request.GetQueryNameValuePairs();

            userCreditCard.UserId = _userService.GetUserByAccessTokenId(creditCardViewModel.AcessTokenId).UserId;

            try
            {
                if (parameters.Any(x => x.Key == "instantBuy"))
                    _cardCompany.MakeTransactionWithCreditCardToken(userCreditCard);
                else
                {
                    _cardCompany.MakeTransaction(userCreditCard);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (CreditCardTokenException ex)
            {
                LogError(userCreditCard.UserId, ex.Message);
                return ResponseCreditCardTokenException(ex);
            }
            catch (TransactionNotAuthorizedException ex)
            {
                LogError(userCreditCard.UserId, ex.Message);
                return ResponseTransactionNotAuthorizedException(ex);
            }
            catch(ErrorTransactionException ex)
            {
                LogError(userCreditCard.UserId, ex.Message);
                return ResponseErrorTransactionException(ex);
            }
            catch(Exception ex)
            {
                LogError(userCreditCard.UserId, ex.Message);
                return ResponseExceptionGeneric(ex);
            }
        }


        private HttpResponseMessage ResponseCreditCardTokenException(Exception ex)
        {
            return Request.CreateResponse(HttpStatusCode.InternalServerError,
                        new
                        {
                            error = ex.Message
                        });
        }

        private HttpResponseMessage ResponseTransactionNotAuthorizedException(Exception ex)
        {
            return Request.CreateResponse(HttpStatusCode.InternalServerError,
                        new
                        {
                            error = ex.Message
                        });
        }


        private HttpResponseMessage ResponseErrorTransactionException(Exception ex)
        {
            return Request.CreateResponse(HttpStatusCode.InternalServerError,
                        new
                        {
                            error = ex.Message
                        });
        }

        private HttpResponseMessage ResponseExceptionGeneric(Exception ex)
        {
            return Request.CreateResponse(HttpStatusCode.InternalServerError,
                        new
                        {
                            error = ex.Message
                        });
        }


        private void LogError(int userId, string message)
        {
            TransactionCreditCardError transactionCreditCardError = new TransactionCreditCardError
            {
                UserId = userId,
                ErrorDescription = message
            };

            _transactionCreditCardErrorService.AddTransactionCreditCardError(transactionCreditCardError);
        }

    }
}
