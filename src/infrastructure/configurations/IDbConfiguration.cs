namespace infrastructure.configurations
{
    public interface IDbConfiguration
    {
        string ConnectionString { get; }
    }

    public class DbConfiguration : IDbConfiguration
    {
        public string ConnectionString { get; set; }
    }
}
