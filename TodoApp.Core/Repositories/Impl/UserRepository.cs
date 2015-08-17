using System.Linq;
using NHibernate;
using NHibernate.Linq;
using TodoApp.Core.Entities;

namespace TodoApp.Core.Repositories.Impl
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly ISession _session;

        public UserRepository(ISession session)
        {
            _session = session;
        }

        public User GetByEmail(string email)
        {
            using (_session.BeginTransaction())
            {
                return _session.Query<User>().SingleOrDefault(x => x.Email == email);
            }
        }

        public void Save(User user)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(user);
                transaction.Commit();
            }
        }
    }
}
