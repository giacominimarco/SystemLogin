using ProjetoLogin.API.Models;
using System.Collections.Generic;

namespace ProjetoLogin.API.Repositories
{
    public interface IUserRepository
    {
        User? GetByUsername(string username);
        List<User> GetAll(int page, int pageSize);
        int Count();
    }
}
