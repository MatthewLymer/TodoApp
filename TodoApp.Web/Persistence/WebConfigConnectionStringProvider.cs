using System.Configuration;
using TodoApp.Core.Persistence;

namespace TodoApp.Web.Persistence
{
    public class WebConfigConnectionStringProvider : IConnectionStringProvider
    {
        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            }
        }
    }
}