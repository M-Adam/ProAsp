using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProAsp.Data;
using ProAsp.Data.Models;
using ProAsp.Data.Repository;

namespace ProAsp.Core.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();

        User GetUser(Guid guid);
        User GetUser(string username);
    }

    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            this._userRepository = userRepository;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return this._userRepository.GetAll();
        }

        public User GetUser(Guid guid)
        {
            return this._userRepository.GetById(guid);
        }

        public User GetUser(string username)
        {
            return this._userRepository.GetSingle(x => x.Name == username);
        }
    }
}
