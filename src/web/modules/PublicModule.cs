using infrastructure.repositories;
using infrastructure.tables;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Cryptography;
using Nancy.ModelBinding;
using Nancy.Security;
using web.models;

namespace web.modules
{
    public class PublicModule : NancyModule
    {
        public PublicModule(IReadRepository readRepository, IWriteRepository writeRepository, CryptographyConfiguration cryptographyConfiguration)
        {
            Get("/404", _ => View["404.html"]);
            Get("/Login", _ =>
            {
                if (this.Context.CurrentUser.IsAuthenticated())
                    return Response.AsRedirect("/");
                return View["login.html", new LoginModel()];
            });

            Post("/Login", _ =>
            {
                var login = this.Bind<LoginModel>();
                var encryptedPassword = cryptographyConfiguration.EncryptionProvider.Encrypt(login.Password);
                var user = readRepository.Get<LoginDb>(x => x.Email == login.Email && x.Password == encryptedPassword);

                if (user == null)
                    return null;

                return this.LoginAndRedirect(user.UniqueIdentifier, fallbackRedirectUrl:"/");
            });
            Get("/Register", _ => View["register.html"]);
            Get("/forgot-password", _ => View["forgot-password.html"]);
            Get("/blank", _ => View["blank.html"]);
            Get("/Buttons", _ => View["buttons.html"]);
            Get("/Cards", _ => View["cards.html"]);
            Get("/Charts", _ => View["charts.html"]);
            Get("/Tables", _ => View["tables.html"]);
            Get("/utilities-color", _ => View["utilities-color.html"]);
            Get("/utilities-border", _ => View["utilities-border.html"]);
            Get("/utilities-animation", _ => View["utilities-animation.html"]);
            Get("/utilities-other", _ => View["utilities-other.html"]);
        }
    }
}
