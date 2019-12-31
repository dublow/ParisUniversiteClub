using System;
using System.Security.Claims;
using System.Security.Principal;
using infrastructure.repositories;
using infrastructure.tables;
using Nancy;
using Nancy.Authentication.Forms;

namespace web.auth
{
    public class UserMapper : IUserMapper
    {
        private readonly IReadRepository _readRepository;

        public UserMapper(IReadRepository readRepository)
        {
            _readRepository = readRepository;
        }
        public ClaimsPrincipal GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            var user = _readRepository.Get<LoginDb>(x => x.UniqueIdentifier == identifier);
            if (user == null)
                return null;

            return new GenericPrincipal(new GenericIdentity(user.Username), new[] {user.Role.ToString()});
        }
    }
}
