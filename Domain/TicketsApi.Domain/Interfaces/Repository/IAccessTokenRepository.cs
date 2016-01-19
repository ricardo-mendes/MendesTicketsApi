using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsApi.Domain.Models;

namespace TicketsApi.Domain.Interfaces.Repository
{
    public interface IAccessTokenRepository
    {
        bool TokenExist(Guid accessTokenId);
        void AddAccessToken(AccessToken token);
        void DeleteAccessToken(Guid accessTokenId);
        AccessToken GetAccessTokenByUserId(int userId);

        void Save();
    }
}
