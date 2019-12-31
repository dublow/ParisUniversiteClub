using SQLite;

namespace infrastructure.tables
{
    [Table("Login")]
    public class LoginDb : ITable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(80), NotNull]
        public string Email { get; set; }
        [MaxLength(50), NotNull]
        public string Username { get; set; }
        [MaxLength(64), NotNull]
        public string Password { get; set; }
        [NotNull]
        public int AccountType { get; set; }
    }
}
