using business;
using web.extensions;

namespace web.models
{
    public class RegisterModel
    {
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Email { get; set; }
        public string Birthday { get; set; }
        public string GameDate { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool Member { get; set; }
        public bool Optin { get; set; }

        public RegisterBusinessModel ToRegisterBusinessModel()
        {
            return new RegisterBusinessModel(
                Lastname.NormalizeName(),
                Firstname.NormalizeName(),
                Email.ToLower(),
                Birthday.ToDateTime(),
                GameDate.ToDateTime(),
                Position.NormalizeName(),
                Phone,
                Address,
                Member,
                Optin);
        }
    }
}
