using System;
using SQLite;

namespace infrastructure.tables
{
    [Table("Register")]
    public class RegisterDb : ITable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50), NotNull]
        public string Lastname { get; set; }
        [MaxLength(50), NotNull]
        public string Firstname { get; set; }
        [MaxLength(80), NotNull]
        public string Email { get; set; }
        [NotNull]
        public DateTime Birthday { get; set; }
        [NotNull]
        public DateTime GameDate { get; set; }
        [MaxLength(30), NotNull]
        public string Position { get; set; }
        [MaxLength(20), NotNull]
        public string Phone { get; set; }
        [MaxLength(150), NotNull]
        public string Address { get; set; }
        [NotNull]
        public bool Member { get; set; }
        [NotNull]
        public bool Optin { get; set; }
    }
}
