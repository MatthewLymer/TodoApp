using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using TodoApp.Core.Entities;

namespace TodoApp.Core.Persistence.Mapping
{
    public class TodoMapping : ClassMapping<Todo>
    {
        public TodoMapping()
        {
            Table("Todos");

            Id(x => x.Id, m => {
                m.Column("Id");
                m.Access(Accessor.Field);
                m.Type(new GuidType());
            });

            Property(x => x.Content, m => {
                m.Column("Content");
                m.NotNullable(true);
            });

            Property(x => x.IsCompleted, m => m.Column("IsCompleted"));

            Property(x => x.DateCreated, m => m.Column("DateCreated"));
        }
    }
}
