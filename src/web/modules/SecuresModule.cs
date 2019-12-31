using infrastructure.repositories;
using Nancy;
using Nancy.Security;

namespace web.modules
{
    public class SecuresModule : NancyModule
    {
        public SecuresModule(IReadRepository readRepository, IWriteRepository writeRepository)
        {
            this.RequiresAuthentication();
            Get("/", _=> View["home.html"]);
        }
    }
}
