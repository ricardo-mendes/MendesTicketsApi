using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsApi.Domain.Interfaces;
using TicketsApi.Domain.Interfaces.Repository;
using TicketsApi.Domain.Models;

namespace TicketsApi.Domain.Services
{
    public class AccessTokenService : IAccessTokenService
    {
        private IAccessTokenRepository _accessTokenRepository;

        public AccessTokenService(IAccessTokenRepository tokenRepository)
        {
            _accessTokenRepository = tokenRepository;
        }

        public void DeleteAccessToken(Guid accessTokenId)
        {
            _accessTokenRepository.DeleteAccessToken(accessTokenId);
            _accessTokenRepository.Save();
        }

        public Guid GenerateAccessTokenForUser(User user)
        {
            AccessToken tokenDeleted = _accessTokenRepository.GetAccessTokenByUserId(user.UserId);
            if(tokenDeleted != null)
                _accessTokenRepository.DeleteAccessToken(tokenDeleted.AccessTokenId);

            AccessToken newToken = new AccessToken();
            newToken.AccessTokenId = Guid.NewGuid();
            newToken.UserId = user.UserId;

            _accessTokenRepository.AddAccessToken(newToken);

            _accessTokenRepository.Save();

            return newToken.AccessTokenId;
        }

        public AccessToken GetAccessTokenByUserId(int userId)
        {
            return _accessTokenRepository.GetAccessTokenByUserId(userId);
        }

        public bool TokenIsValid(Guid accessTokenId)
        {
            return _accessTokenRepository.TokenExist(accessTokenId);
        }
    }
}
