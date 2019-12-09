using Nancy;

namespace NancyBootstrapAdmin2.Modules
{
    public class PagesModule : NancyModule
    {
        public PagesModule()
        {
            Get("/", _=> View["home.html"]);
            Get("/404", _ => View["404.html"]);
            Get("/Login", _ => View["login.html"]);
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
