using business;
using infrastructure.repositories;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using web.models;

namespace web.modules
{
    public class UserModule : NancyModule
    {
        public UserModule(RegisterBusiness registerBusiness)
        {
            this.RequiresAuthentication();

            Get("/Register", _ => View["register.html", new RegisterModel()]);

            Post("/Register", _ =>
            {
                var model = this.Bind<RegisterModel>();

                registerBusiness.CreateRegister(model.ToRegisterBusinessModel());

                return Response.AsRedirect("/");
            });
        }
    }
}
