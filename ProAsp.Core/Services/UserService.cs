using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProAsp.Data;
using ProAsp.Data.Models;

namespace ProAsp.Core.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
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
    }
}
