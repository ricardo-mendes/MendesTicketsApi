using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsApi.Domain.Exceptions;
using TicketsApi.Domain.Interfaces;
using TicketsApi.Domain.Interfaces.Repository;
using TicketsApi.Domain.Models;

namespace TicketsApi.Domain.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddUser(User user)
        {
            if (EmailAlreadyExists(user.Email))
                throw new EmailAlreadyExistsException();

            if(CpfAlreadyExists(user.Cpf))
                throw new CpfAlreadyExistsException();

            _userRepository.AddUser(user);
            _userRepository.Save();
        }

        public User GetUserByAccessTokenId(Guid accessTokenId)
        {
            var user  = _userRepository.GetUserByAccessTokenId(accessTokenId);

            if (user == null)
                throw new Exception("Erro no token de acesso");

            return user;
        }

        public User GetUserByCreditCardTokenId(Guid creditCardTokenId)
        {
            return _userRepository.GetUserByCreditCardTokenId(creditCardTokenId);
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            var user = _userRepository.GetUserByEmailAndPassword(email, password);

            if (user == null)
                throw new UserDoesNotExistException();

            return user;
        }

        private bool EmailAlreadyExists(string email)
        {
            if (_userRepository.GetUserByEmail(email) != null)
                return true;

            return false;
        }

        private bool CpfAlreadyExists(string cpf)
        {
            if (_userRepository.GetUserByCpf(cpf) != null)
                return true;

            return false;
        }


    }
}
