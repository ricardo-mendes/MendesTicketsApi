using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsApi.Domain.Interfaces.Repository;
using TicketsApi.Domain.Models;
using TicketsApi.EfData.Context;

namespace TicketsApi.EfData.Repositories
{
    public class AccessTokenRepository : IAccessTokenRepository
    {
        private TicketsContext _context;

        public AccessTokenRepository(TicketsContext context)
        {
            _context = context;
        }

        public void AddAccessToken(AccessToken token)
        {
            _context.AccessTokens.Add(token);
        }

        public bool TokenExist(Guid accessTokenId)
        {
            var token = _context.AccessTokens.Where(t => t.AccessTokenId  == accessTokenId).FirstOrDefault();

            if(token != null)
                return true;

            return false;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void DeleteAccessToken(Guid accessTokenId)
        {
            AccessToken token = _context.AccessTokens.Where(t => t.AccessTokenId == accessTokenId).FirstOrDefault();
            _context.AccessTokens.Remove(token);
        }

        public AccessToken GetAccessTokenByUserId(int userId)
        {
            var token = _context.AccessTokens.Where(t => t.UserId == userId).FirstOrDefault();

            return token;
        }
    }
}
