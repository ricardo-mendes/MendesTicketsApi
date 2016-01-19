using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TicketsApi.Domain.Interfaces;
using TicketsApi.Domain.Services;
using TicketsApi.EfData.Context;
using TicketsApi.EfData.Repositories;

namespace TicketsApi.Web.Controllers
{
    public class AccessTokenController : ApiController
    {
        private IAccessTokenService _tokenService;

        public AccessTokenController()
        {
            _tokenService = new AccessTokenService(new AccessTokenRepository(new TicketsContext()));
        }

        public HttpResponseMessage Post([FromBody] string accessTokenId)
        {
            if (_tokenService.TokenIsValid(Guid.Parse(accessTokenId)))
                return Request.CreateResponse(HttpStatusCode.OK);
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        public HttpResponseMessage Delete([FromUri] string accessTokenId)
        {
            try
            {
                _tokenService.DeleteAccessToken(Guid.Parse(accessTokenId));
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
