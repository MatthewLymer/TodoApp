using TodoApp.Core.Entities;

namespace TodoApp.Core.Repositories
{
    public interface IUserRepository
    {
        User GetByEmail(string email);

        void Save(User user);
    }
}
