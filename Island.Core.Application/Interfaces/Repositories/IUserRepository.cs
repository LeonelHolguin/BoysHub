using Island.Core.Application.ViewModels.User;
using Island.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Island.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<User?> LoginAsync(LoginViewModel userLogin);
        Task<User> RegisterAsync(User userRegister);
        Task<bool> UserNameExistsAsync(string userName);
    }
}
