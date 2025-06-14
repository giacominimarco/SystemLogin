using Microsoft.EntityFrameworkCore;
using ProjetoLogin.API.Data;
using ProjetoLogin.API.Models;

namespace ProjetoLogin.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User? GetByUsername(string username)
        {
            return _context.Users
                .AsNoTracking()
                .FirstOrDefault(u => u.Username == username);
        }

        public List<User> GetAll(int page, int pageSize)
        {
            return _context.Users
                .AsNoTracking()
                .OrderBy(u => u.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int Count()
        {
            return _context.Users.Count();
        }
    }
}