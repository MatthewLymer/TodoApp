using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using TodoApp.Core.Entities;

namespace TodoApp.Core.Persistence.Mapping
{
    public class UserMapping : ClassMapping<User>
    {
        public UserMapping()
        {
            Table("Users");

            Id(x => x.Id, m => {
                m.Column("Id");
                m.Access(Accessor.Field);
            });

            Bag<Todo>("_todos", m =>
            {
                m.Access(Accessor.Field);
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("UserId"));
            }, r => r.OneToMany());
        }
    }
}
