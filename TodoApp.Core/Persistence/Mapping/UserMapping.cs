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

            Property(x => x.Email, m => m.Column("Email"));

            Bag(x => x.Todos, m =>
            {
                m.Access(Accessor.Field);
                m.Cascade(Cascade.All);
                m.Key(k => {
                    k.Column("UserId");
                    k.NotNullable(true);
                    k.Update(false);
                });
            }, r => r.OneToMany());
        }
    }
}
