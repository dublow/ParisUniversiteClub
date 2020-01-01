using System.Linq;
using infrastructure.repositories;
using infrastructure.tables;
using Nancy;
using web.extensions;
using web.models;

namespace web.modules
{
    public class AdminModule : NancyModule
    {
        public AdminModule(IReadRepository readRepository)
        {
            this.RequireAdmin();
            
            Get("/", _=>
            {
                return View["home.html"];
            });

            Get("/Registered", _ =>
            {
                var registeredModel = readRepository.Find<RegisterDb>(x => true).Select(x=> new RegisteredModel(x));
                return View["registered.html", registeredModel];
            });
        }
    }
}
