using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using infrastructure.repositories;
using infrastructure.tables;
using Nancy;
using Nancy.ModelBinding;
using web.extensions;
using web.models;
using web.tools;
using Range = web.models.Range;

namespace web.modules
{
    public class AdminModule : NancyModule
    {
        public AdminModule(IReadRepository readRepository, IEmailSender emailSender)
        {
            this.RequireAdmin();
            
            Get("/", _=>
            {
                return View["home.html"];
            });

            Get("/Registered", _ =>
            {
                var registeredModel = readRepository
                    .Find<RegisterDb>(x => true)
                    .Select(x=> new RegisteredModel(x))
                    .ToList();

                var result = new {Filter = new FilteredModel(), Register = registeredModel, Ids = registeredModel.Inline(x => x.Id) };
                return View["registered.html", result];
            });

            Post("/Filtered", _ =>
            {
                if (this.Request.Form["clearFilterSubmit"])
                {
                    return this.Response.AsRedirect("/Registered");
                }

                var model = this.Bind<FilteredModel>();
                var registeredModel = readRepository
                    .Find<RegisterDb>(x => true)
                    .Where(x => FilteredBirthday(x, model.Range, model.BirthdayStart, model.BirthdayEnd)
                                && FilteredBool(x, model.IsOptin, (y, b) => y.Optin == b)
                                && FilteredBool(x, model.IsMember, (y, b) => y.Member == b))
                    .Select(x => new RegisteredModel(x))
                    .ToList();

                var result = new {Filter = model, Register = registeredModel, Ids = registeredModel.Inline(x => x.Id) };
                return View["registered.html", result];
            });

            Post("/Email", _ =>
            {
                var model = this.Bind<EmailModel>();
                var emails = readRepository.Find<RegisterDb>(x => model.RegisterIds.Contains(x.Id)).Inline(x => x.Email);
                return View["email.html", new EmailViewModel(emails)];
            });

            Post("/SendEmail", _ =>
            {
                var model = this.Bind<SendEmailModel>();
                emailSender.Send(model.Subject, model.Message, model.Emails);
                return this.Response.AsRedirect("/registered.html");
            });
        }

        private bool FilteredBirthday(RegisterDb registerDb, Range? range, int yearStart, int yearEnd)
        {
            if (!range.HasValue)
                return true;

            return range switch
            {
                Range.Before => (registerDb.Birthday.Year <= yearStart),
                Range.After => (registerDb.Birthday.Year >= yearStart),
                Range.Between => (registerDb.Birthday.Year >= yearStart && registerDb.Birthday.Year <= yearEnd),
                _ => throw new ArgumentOutOfRangeException(nameof(range), range, null)
            };
        }

        private static bool FilteredBool(RegisterDb registerDb, string value, Func<RegisterDb, bool, bool> filter)
        {
            return 
                string.IsNullOrEmpty(value) 
                || filter(registerDb, bool.Parse(value));
        }
    }
}
