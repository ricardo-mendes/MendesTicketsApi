using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
    
    public class UserController : ApiController
    {
        private IUserService _userService;
        private IAccessTokenService _tokenService;
        private UserMapper _userMapper;

        public UserController()
        {
            TicketsContext context = new TicketsContext();
            _userService = new UserService(new UserRepository(context));
            _tokenService = new AccessTokenService(new AccessTokenRepository(context));
            _userMapper = new UserMapper();
        }


        public HttpResponseMessage Post([FromBody] UserViewModel userVieModel)
        {
            User user = _userMapper.MapToUser(userVieModel); 

            var parameters = Request.GetQueryNameValuePairs();

            if (parameters.Any(x => x.Key == "login"))
                return MakeLogin(user);
            else
            {
                return MakeRegister(user);
            }
        }

        private HttpResponseMessage MakeLogin(User user)
        {
            try
            {
                User userLogged = _userService.GetUserByEmailAndPassword(user.Email, user.Password);

                return ResponseLoginOk(userLogged);
            }
            catch (UserDoesNotExistException ex)
            {
                return ResponseUserDoesNotExistException(ex);
            }
            catch (Exception ex)
            {
                return ResponseExceptionGeneric(ex);
            }
        }

        private HttpResponseMessage MakeRegister(User user)
        {
            try
            {
                _userService.AddUser(user);
                return MakeLogin(user);
            }
            catch (EmailAlreadyExistsException ex)
            {
                return ResponseEmailAlreadyExistsException(ex);
            }
            catch (CpfAlreadyExistsException ex)
            {
                return ResponseCpfAlreadyExistsException(ex);
            }
            catch (Exception ex)
            {
                return ResponseExceptionGeneric(ex);
            }
        }

        private HttpResponseMessage ResponseLoginOk(User user)
        {
            string accessToken = _tokenService.GenerateAccessTokenForUser(user).ToString();

            return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        token = accessToken
                    });
        }

        private HttpResponseMessage ResponseEmailAlreadyExistsException(EmailAlreadyExistsException ex)
        {
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                        new
                        {
                            error = ex.Message
                        });
        }

        private HttpResponseMessage ResponseCpfAlreadyExistsException(CpfAlreadyExistsException ex)
        {
            return Request.CreateResponse(HttpStatusCode.BadRequest,
                        new
                        {
                            error = ex.Message
                        });
        }

        private HttpResponseMessage ResponseUserDoesNotExistException(Exception ex)
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

    }
}
