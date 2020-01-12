using infrastructure.tables;
using web.extensions;

namespace web.models
{
    public class RegisteredModel
    {
        public int Id { get; }
        public string Lastname { get; }
        public string Firstname { get; }
        public string Email { get; }
        public string Birthday { get; }
        public string GameDate { get; }
        public string Position { get; }
        public string Phone { get; }
        public string Address { get; }
        public string Member { get; }
        public string Optin { get; }

        public RegisteredModel(RegisterDb model)
        {
            Id = model.Id;
            Lastname = model.Lastname;
            Firstname = model.Firstname;
            Email = model.Email;
            Birthday = model.Birthday.ToString("dd/MM/yyyy");
            GameDate = model.GameDate.ToString("dd/MM/yyyy");
            Position = model.Position;
            Phone = model.Phone;
            Address = model.Address;
            Member = model.Member.ToYesNo();
            Optin = model.Optin.ToYesNo();
        }
    }
}
