using System;
using infrastructure.repositories;
using infrastructure.tables;

namespace business
{
    public class RegisterBusiness
    {
        private readonly IWriteRepository _writeRepository;

        public RegisterBusiness(IWriteRepository writeRepository)
        {
            _writeRepository = writeRepository;
        }

        public void CreateRegister(RegisterBusinessModel model)
        {
            _writeRepository.Add(new RegisterDb
            {
                Email = model.Email,
                Address = model.Address,
                Birthday = model.Birthday,
                Firstname = model.Firstname,
                GameDate = model.GameDate,
                Lastname = model.Lastname,
                Member = model.Member,
                Optin = model.Optin,
                Phone = model.Phone,
                Position = model.Position
            });
        }
    }

    public class RegisterBusinessModel
    {
        public string Lastname { get; }
        public string Firstname { get;  }
        public string Email { get; }
        public DateTime Birthday { get; }
        public DateTime GameDate { get; }
        public string Position { get; }
        public string Phone { get; }
        public string Address { get; }
        public bool Member { get; }
        public bool Optin { get; }

        public RegisterBusinessModel(
            string lastname, 
            string firstname, 
            string email, DateTime birthday, 
            DateTime gameDate, 
            string position, 
            string phone, 
            string address, 
            bool member, 
            bool optin)
        {
            Lastname = lastname;
            Firstname = firstname;
            Email = email;
            Birthday = birthday;
            GameDate = gameDate;
            Position = position;
            Phone = phone;
            Address = address;
            Member = member;
            Optin = optin;
        }
    }
}
