using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsApi.Domain.Models;

namespace TicketsApi.Domain.Interfaces
{
    public interface IAccessTokenService
    {
        bool TokenIsValid(Guid accessTokenId);
        Guid GenerateAccessTokenForUser(User user);
        void DeleteAccessToken(Guid accessTokenId);
        AccessToken GetAccessTokenByUserId(int userId);
    }
}
